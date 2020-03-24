using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ET.Domain.Domains
{
    public interface IGroupRepository
    {
        Task<int> NextId();
        Task<Group> Get(int id);
        Task CreateAsync(Group user);
    }
}
