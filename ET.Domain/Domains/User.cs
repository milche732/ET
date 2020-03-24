using ET.Domain.Domains.Events;
using ET.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;

        private User(int id, string name)
        {
            Id = id;
            Name = name;            
        }
        public static User New(int id, string name)
        {
            User user = new User(id, name);
            user.AddDomainEvent(new NewUserCreated(id, name));
            return user;
        }

        public UserInGroup AddToGroup(Group group)
        {
            UserInGroup userInGroup = UserInGroup.New(Id, group.Id);
            this.AddDomainEvent(new UserToGroupAdded(Name,group.Name));
            return userInGroup; 
        }
    }
}
