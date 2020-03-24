using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains.Events
{
    public class NewUserCreated : DomainEvent
    {
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public NewUserCreated(int userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
