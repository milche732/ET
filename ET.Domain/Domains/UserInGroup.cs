using ET.Domain.Domains.Events;
using ET.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains
{
    public class UserInGroup : Entity
    {
        public int UserId { get; }
        public int GroupId { get; }
        public bool InActive { get; private set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;

        public User User { get; set; } 
        public Group Group { get; set; } 
        private UserInGroup(int userId, int groupId)
        {
            UserId = userId;
            GroupId = groupId;            
        }

        public static UserInGroup New(int userId, int groupId)
        {
            UserInGroup role = new UserInGroup(userId, groupId);
            return role;
        }

        public void SetInactive()
        {
            InActive = true;
            //fire domain event here
        }

        public void SetActive()
        {
            InActive = false;
            //fire domain event here
        }
    }
}
