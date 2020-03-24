using ET.Domain.Domains;
using ET.Infrastructure;
using ET.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ET.Con
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            //options.UseSqlServer("");
            using (IdentityContext context = new IdentityContext())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
                try
                {
                    await context.Database.BeginTransactionAsync();
                    IGroupRepository groupRepository = new GroupRepository(context);
                    int groupId = await groupRepository.NextId();
                    var group = Group.New(groupId, "Alex's Group");


                    IUserRepository userRepository = new UserRepository(context);
                    int userId = await userRepository.NextId();
                    User user = User.New(userId, "Alex");


                    IUserInGroupRepository userInGroupRepository = new UserInGroupRepository(context);
                    var userInGroup = user.AddToGroup(group);

                    await groupRepository.CreateAsync(group);
                    await userRepository.CreateAsync(user);
                    await userInGroupRepository.CreateAsync(userInGroup);

                    await userRepository.UnitOfWork.SaveEntitiesAsync();
                    context.Database.CommitTransaction();
                }
                catch
                {
                    context.Database.RollbackTransaction();
                }
                //Console.WriteLine(context.Database.GenerateCreateScript());

            }
        }
    }
}
