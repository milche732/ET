using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.Web.Application.Commands.Exceptions
{
    public class UserNameAlreadyExistsException:Exception
    {
        public UserNameAlreadyExistsException(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string Message => $"User name {Name} already exists.";
    }
}
