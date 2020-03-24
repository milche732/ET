using ET.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ET.Domain.Domains
{
    public interface IUserInGroupRepository:IRepository<UserInGroup>
    {
        Task CreateAsync(UserInGroup userInGroup);        
    }
}
