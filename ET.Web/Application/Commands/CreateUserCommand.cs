using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ET.Web.Application.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        [DataMember]
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        
        [DataMember]
        [Range(1, 1000000)]
        public int GroupId { get; set; }
    }
}
