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
    public class GroupRepository : IGroupRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return identityContext;
            }
        }
        private readonly IdentityContext identityContext;

        public GroupRepository(IdentityContext identityContext)
        {
            this.identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        }
        public async Task<Group> Get(int id)
        {
            return await identityContext.Groups.FindAsync(id);
        }
               
        public async Task<int> NextId()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await identityContext.Database.ExecuteSqlRawAsync("SELECT @result = (NEXT VALUE FOR group_sequence)", result);
            return (int)result.Value;
        }

        public async Task CreateAsync(Group group)
        {
            await identityContext.Groups.AddAsync(group);
        }
    }
}
