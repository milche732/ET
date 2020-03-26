using ET.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ET.Domain.Domains
{
    public interface IUserInGroupRepository:IRepository<UserInGroup>
    {
        Task<UserInGroup> GetAsync(int userId, int groupId);
        Task CreateAsync(UserInGroup userInGroup);
        void Update(UserInGroup userInGroup);

        Task CreateOrUpdate(UserInGroup userInGroup);

        void Delete(UserInGroup userInGroup);
    }
}
