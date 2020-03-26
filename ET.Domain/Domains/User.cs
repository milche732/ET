using ET.Domain.Domains.Events;
using ET.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ET.Domain.Domains
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public bool InActive { get; private set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;

        public List<UserInGroup> Groups { get; set; }
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
            var uig = Groups.Where(g => g.GroupId == group.Id).FirstOrDefault();
            if (uig != null)
            {
                uig.SetActive();
            }
            else
            {
                uig  = UserInGroup.New(Id, group.Id);
                Groups.Add(uig);
            }
            this.AddDomainEvent(new UserToGroupAdded(Name, group.Name));
            return uig;
        }

        public UserInGroup RemoveFromGroup(Group group)
        {
            var uig = Groups.Where(g => g.GroupId == group.Id).FirstOrDefault();
            if (uig != null)
                uig.SetInactive();
            this.AddDomainEvent(new UserFromGroupRemoved(Name, group.Name));
            return uig;
        }
        public void SetInactive()
        {
            InActive = true;
            //fire domain event here
        }
    }
}
