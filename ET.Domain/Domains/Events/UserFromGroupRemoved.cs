using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains.Events
{
    public class UserFromGroupRemoved : DomainEvent
    {
        public string UserName { get; private set; }
        public string GroupName { get; private set; }
        public UserFromGroupRemoved(string userName, string groupName)
        {
            UserName = userName;
            GroupName = groupName;
        }
    }
}
