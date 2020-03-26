using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ET.Web.Application.Commands;
using ET.Web.Application.Commands.Exceptions;
using ET.Web.Application.Queries.Dto;
using ET.Web.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ET.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IMediator _mediator;

        public GroupsController(ILogger<GroupsController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var groups = await _mediator.Send(new QueryAllGroupsCommand());
            return Ok(groups);
        }

        [HttpDelete]
        [Route("{userId}/{groupId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> RemoveFromGroup(int userId, int groupId)
        {
            var result = await _mediator.Send(new RemoveUserFromGroupCommand { UserId = userId, GroupId = groupId });

            if (result)
                return Ok();
            else 
                return NotFound();
        }

        [HttpPut]        
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> AddToGroup([FromBody]AddUserToGroupCommand command)
        {
            var result = await _mediator.Send(command);

            if (result)
                return Ok();
            else 
                return NotFound();
        }
    }
}
