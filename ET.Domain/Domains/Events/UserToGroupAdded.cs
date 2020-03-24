using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains.Events
{
    public class UserToGroupAdded : DomainEvent
    {
        public string UserName { get; private set; }
        public string GroupName { get; private set; }
        public UserToGroupAdded(string userName, string groupName)
        {
            UserName = userName;
            GroupName = groupName;
        }
    }
}
