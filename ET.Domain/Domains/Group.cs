using ET.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Domain.Domains
{
    public class Group : Entity
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public bool InActive { get; private set; }
        public DateTime DateCreated { get; private set; } = DateTime.Now;

        public IList<UserInGroup> Users { get; set; }
        private Group(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public static Group New(int id, string name)
        {
            return new Group(id, name);
        }

        public void SetInactive()
        {
            InActive = true;
            //fire domain event here
        }
    }
}
