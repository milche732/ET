using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.Web.Application.Commands.Exceptions
{
    public class GroupNotFoundException:Exception
    {
        private readonly int groupid;

        public GroupNotFoundException(int groupid)
        {
            this.groupid = groupid;
        }
        public override string Message => $"Group {groupid} not found.";
    }
}
