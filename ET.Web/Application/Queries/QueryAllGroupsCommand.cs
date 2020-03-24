using ET.Web.Application.Commands;
using ET.Web.Application.Queries.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.Web.Application.Query
{
    public class QueryAllGroupsCommand : IRequest<IEnumerable<GroupDto>>
    {

        public QueryAllGroupsCommand()
        {
        }
    }
}
