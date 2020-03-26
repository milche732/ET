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
    public class RemoveUserFromGroupCommandHandler : IRequestHandler<RemoveUserFromGroupCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IUserInGroupRepository userInGroupRepository;

        public RemoveUserFromGroupCommandHandler(IUserRepository userRepository,
            IGroupRepository groupRepository, IUserInGroupRepository userInGroupRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            this.userInGroupRepository = userInGroupRepository ?? throw new ArgumentNullException(nameof(userInGroupRepository));
        }

        public async Task<bool> Handle(RemoveUserFromGroupCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.Get(request.UserId);
            if (user != null)
            {
                var group = await groupRepository.Get(request.GroupId);
                if (group != null)
                {
                    var userInGroup = user.RemoveFromGroup(group);
                    //userInGroupRepository.Update(userInGroup);
                    return true;
                }
            }

            return false;
        }
    }
}
