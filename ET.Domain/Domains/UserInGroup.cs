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
        private UserInGroup(int userId, int groupId)
        {
            UserId = userId;
            GroupId = groupId;            
        }

        public static UserInGroup New(int user, int group)
        {
            UserInGroup role = new UserInGroup(user, group);

            return role;
        }
    }
}
