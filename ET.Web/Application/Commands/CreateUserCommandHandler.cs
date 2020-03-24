using ET.Domain.Domains;
using ET.Web.Application.Commands.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ET.Web.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IUserInGroupRepository userInGroupRepository;

        public CreateUserCommandHandler(IUserRepository userRepository,
            IGroupRepository groupRepository, IUserInGroupRepository userInGroupRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            this.userInGroupRepository = userInGroupRepository ?? throw new ArgumentNullException(nameof(userInGroupRepository));
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Group group = await groupRepository.Get(request.GroupId);
            if (group != null)
            {
                User user = await userRepository.Get(request.Name);
                if (user == null)
                {                    
                    var id = await userRepository.NextId();
                    user = User.New(id, request.Name);

                    await userRepository.CreateAsync(user);

                    UserInGroup userInGroup = user.AddToGroup(group);

                    await userInGroupRepository.CreateAsync(userInGroup);

                    return id;
                }
                else
                    throw new UserNameAlreadyExistsException(request.Name);
            }
            else
                throw new GroupNotFoundException(request.GroupId);
        }
    }

   
}
