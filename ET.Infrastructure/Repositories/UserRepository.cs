using ET.Domain.Domains;
using ET.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ET.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return identityContext;
            }
        }
        private readonly IdentityContext identityContext;

        public UserRepository(IdentityContext identityContext)
        {
            this.identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        }
        public async Task<User> Get(int id)
        {
            var user =  await identityContext.Users.FindAsync(id);
            if(user != null)
              await identityContext.Entry(user).Collection(x => x.Groups).LoadAsync();
            return user;
        }

        public async Task<User> Get(string name)
        {
            return await identityContext.Users.FirstOrDefaultAsync(x=>x.Name == name);
        }

        public async Task<int> NextId()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await identityContext.Database.ExecuteSqlRawAsync("SELECT @result = (NEXT VALUE FOR user_sequence)", result);
            return (int)result.Value;
        }

        public async Task CreateAsync(User user)
        {
            await identityContext.Users.AddAsync(user);
        }
    }
}
