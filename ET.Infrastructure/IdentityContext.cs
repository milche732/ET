using ET.Domain.Domains;
using ET.Domain.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ET.Infrastructure
{
    public class IdentityContext : DbContext, IUnitOfWork
    {
        private string connectionString;
        private IDbContextTransaction _currentTransaction;
        public const string DEFAULT_SCHEMA = "identity";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connectionString))
                optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserInGroup> UserInGroup { get; set; }
        public bool HasActiveTransaction => _currentTransaction != null;

        public IdentityContext()
        {
        }

        public IdentityContext(string connection)
        {
            connectionString = connection;
        }
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            //Database.EnsureDeletedAsync().Wait();
            //Database.EnsureCreatedAsync().Wait();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("user_sequence").IncrementsBy(1);
            modelBuilder.HasSequence("group_sequence").IncrementsBy(1);
            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupMemberEntityTypeConfiguration());
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> orderConfiguration)
        {
            orderConfiguration.ToTable("groups");
            orderConfiguration.Property(b => b.Name).HasMaxLength(50);
            orderConfiguration.Property(b => b.Id).ValueGeneratedNever();
        }
    }

    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> orderConfiguration)
        {
            orderConfiguration.ToTable("users");
            orderConfiguration.HasIndex(b => b.Name);
            orderConfiguration.Property(b => b.Name).HasMaxLength(50);
            orderConfiguration.Property(b => b.Id).ValueGeneratedNever();
           // orderConfiguration.HasMany<UserInGroup>().WithOne();
        }
    }

    class GroupMemberEntityTypeConfiguration : IEntityTypeConfiguration<UserInGroup>
    {
        public void Configure(EntityTypeBuilder<UserInGroup> orderConfiguration)
        {
            orderConfiguration.ToTable("user_in_group");

            orderConfiguration.HasKey(x => new { x.GroupId, x.UserId });

            orderConfiguration
                 .HasOne<User>(sc => sc.User)
                 .WithMany(s => s.Groups)
                 .HasForeignKey(sc => sc.UserId);

            orderConfiguration
               .HasOne<Group>(sc => sc.Group)
               .WithMany(s => s.Users)
               .HasForeignKey(sc => sc.GroupId);

            //orderConfiguration.HasOne<Group>().WithMany(x => x.Users).HasForeignKey(x => x.GroupId);
            //orderConfiguration.HasOne<User>().WithMany(x => x.Groups).HasForeignKey(x => x.UserId);

        }
    }
}

