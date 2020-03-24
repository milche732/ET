﻿using ET.Web.Application.Commands;
using ET.Web.Application.Queries.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET.Web.Application.Query
{
    public class QueryUserCommand : IRequest<UserDto>
    {
        public string Name { get; }

        public QueryUserCommand(string name)
        {
            Name = name;
        }
    }
}
