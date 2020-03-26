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
    public class QueryAllUsersCommandHandler : IRequestHandler<QueryAllUsersCommand,IEnumerable<UserDto>>
    {
        private readonly IOptions<SqlSettings> options;

        public QueryAllUsersCommandHandler(IOptions<SqlSettings> options)
        {
            this.options = options;
        }
        public async Task<IEnumerable<UserDto>> Handle(QueryAllUsersCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<UserDto> users = null;
            using (IDbConnection db = new SqlConnection(options.Value.ConnectionString))
            {
                var results = await db.QueryMultipleAsync(
                          @"select
                                u.Id
                                ,u.Name
                                ,u.DateCreated
                                ,u.InActive
                            from users u   
                                where u.InActive =  0

                           select g.Id
                                ,g.Name
                                ,ug.UserId
                            from groups g  
                            join user_in_group ug on g.Id = ug.GroupId        
                            join users u on u.Id = ug.UserId
                            where g.InActive = 0 and ug.InActive = 0"
                            );

                users = await results.ReadAsync<UserDto>();
                
                var groups = await results.ReadAsync<UserGroupDto>();
                
                foreach(var user in users)
                {
                    user.Groups = groups.Where(x => x.UserId == user.Id).Select(x=>new GroupDto { Id = x.Id, Name = x.Name });
                }
            }

            return users;
        }       
    }

    public class UserGroupDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
