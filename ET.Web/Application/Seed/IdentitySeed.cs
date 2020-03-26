using ET.Domain.Domains;
using ET.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.Web.Application.Seed
{
    public class IdentitySeed
    {
        private readonly IdentityContext identityContext;
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IUserInGroupRepository userInGroupRepository;

        public IdentitySeed(IdentityContext identityContext,
            IUserRepository userRepository,
            IGroupRepository groupRepository,
            IUserInGroupRepository userInGroupRepository)
        {
            this.identityContext = identityContext ?? throw new ArgumentNullException(nameof(identityContext));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            this.userInGroupRepository = userInGroupRepository ?? throw new ArgumentNullException(nameof(userInGroupRepository));
        }
        public async Task EnsureMigrationOfContext()
        {
            //identityContext.Database.EnsureDeleted();
            if (identityContext.Database.EnsureCreated())
            {                
                List<Group> groups = new List<Group>();
                foreach (string gName in new string[] { "Group A", "Group B" })
                {
                    int groupId = await groupRepository.NextId();
                    var group = Group.New(groupId, gName);
                    await groupRepository.CreateAsync(group);
                    groups.Add(group);
                }

                Random r = new Random();
                foreach (string gName in new string[] { "Peter", "Linda", "Mark", "Jim", "Megan" })
                {
                    int userId = await userRepository.NextId();
                    
                    var user = await userRepository.CreateAsync(User.New(userId, gName));

                    int i = r.Next(0, 1);

                    user.AddToGroup(groups[i]);                    
                }

                await userRepository.UnitOfWork.SaveEntitiesAsync();
            }
        }
    }

}
