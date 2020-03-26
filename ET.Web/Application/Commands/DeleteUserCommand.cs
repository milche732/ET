using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ET.Web.Application.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {        
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
