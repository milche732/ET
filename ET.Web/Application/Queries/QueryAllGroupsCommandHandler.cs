using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;
using ET.Web.Application.Query;
using ET.Web.Application.Commands;
using ET.Web.Application.Queries.Dto;

namespace ET.Web.Application.Query
{
    public class QueryAllGroupsCommandHandler : IRequestHandler<QueryAllGroupsCommand,IEnumerable<GroupDto>>
    {
        private readonly IOptions<SqlSettings> options;

        public QueryAllGroupsCommandHandler(IOptions<SqlSettings> options)
        {
            this.options = options;
        }
        public async Task<IEnumerable<GroupDto>> Handle(QueryAllGroupsCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<GroupDto> groups = null;
            using (IDbConnection db = new SqlConnection(options.Value.ConnectionString))
            {
                var results = await db.QueryMultipleAsync(
                          @"select
                                g.Id
                                ,g.Name
                            from groups g"                             
                            );

                groups = await results.ReadAsync<GroupDto>();
            }

            return groups;
        }       
    }
}
