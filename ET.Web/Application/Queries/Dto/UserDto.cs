using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.Web.Application.Queries.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }        
        public IEnumerable<GroupDto> Groups { get; set; }
    }
}
