using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ET.Web.Application.Commands
{
    public class AddUserToGroupCommand : IRequest<bool>
    {
        [DataMember]
        [Required]
        [Range(1, 1000000)]
        public int UserId { get; set; }
        
        [DataMember]
        [Range(1, 1000000)]
        public int GroupId { get; set; }
    }
}
