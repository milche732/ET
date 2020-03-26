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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.Get(request.UserId);
            if (user != null)
            {
                user.SetInactive();
                return true;
            }
            else
                return false;
        }
    }
}
