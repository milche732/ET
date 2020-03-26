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

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await userRepository.Get(request.Name);
            if (user == null)
            {
                var id = await userRepository.NextId();
                user = User.New(id, request.Name);

                await userRepository.CreateAsync(user);

                return id;
            }
            else
                throw new UserNameAlreadyExistsException(request.Name);
        }
    }
}
