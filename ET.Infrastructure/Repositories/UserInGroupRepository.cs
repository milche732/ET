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
    public class UserInGroupRepository : IUserInGroupRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return identityContext;
            }
        }
        private readonly IdentityContext identityContext;

        public UserInGroupRepository(IdentityContext identityContext)
        {
            this.identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
        }
        public async Task CreateAsync(UserInGroup userInGroup)
        {
            await identityContext.UserInGroup.AddAsync(userInGroup);
        }
    }
}
