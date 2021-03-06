﻿using ET.Domain.Domains;
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

        public async Task<UserInGroup> GetAsync(int userId, int groupId)
        {
            UserInGroup userInGroup = await identityContext.UserInGroup.FindAsync(userId, groupId);
            return userInGroup;
        }

        public void Delete(UserInGroup userInGroup)
        {
            identityContext.UserInGroup.Remove(userInGroup);
        }

        public void Update(UserInGroup userInGroup)
        {
            identityContext.UserInGroup.Update(userInGroup);
        }

        public async Task CreateOrUpdate(UserInGroup userInGroup)
        {
            var group = await GetAsync(userInGroup.UserId, userInGroup.GroupId);
            if (group == null)
                await CreateAsync(userInGroup);
            else
                Update(userInGroup);
        }
    }
}
