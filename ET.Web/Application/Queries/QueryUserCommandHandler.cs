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
    public class QueryUserCommandHandler : IRequestHandler<QueryUserCommand, UserDto>
    {
        private readonly IOptions<SqlSettings> options;

        public QueryUserCommandHandler(IOptions<SqlSettings> options)
        {
            this.options = options;
        }
        public async Task<UserDto> Handle(QueryUserCommand request, CancellationToken cancellationToken)
        {
            UserDto user = null;
            using (IDbConnection db = new SqlConnection(options.Value.ConnectionString))
            {
                var results = await db.QueryMultipleAsync(
                          @"select u.Name
                                ,u.DateCreated
                            from users u                             
                           where u.Name = @name

                           select g.Id
                                ,g.Name  
                            from groups g  
                            join user_in_group ug on g.Id = ug.GroupId        
                            join users u on u.Id = ug.UserId
                            where u.Name = @name", new { name =  request.Name });

                user = await results.ReadFirstOrDefaultAsync<UserDto>();
                if(user!=null)
                    user.Groups = await results.ReadAsync<GroupDto>();
            }

            return user;
        }       
    }

   

    
}
