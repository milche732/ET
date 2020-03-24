using ET.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ET.Domain.Domains
{
    public interface IUserRepository:IRepository<User>
    {
        Task<int> NextId();
        Task CreateAsync(User user);        
        Task<User> Get(int id);
        Task<User> Get(string name);
    }
}
