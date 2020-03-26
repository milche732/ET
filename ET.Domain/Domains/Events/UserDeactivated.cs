using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains.Events
{
    public class UserDeactivated : DomainEvent
    {
        public int UserId { get; }
        public string Name { get; }
        public UserDeactivated(int userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
