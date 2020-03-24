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
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IMediator _mediator;

        public IdentityController(ILogger<IdentityController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{name}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<ActionResult> Get(string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest();
            var user = await _mediator.Send(new QueryUserCommand(name));
            if (user != null)
                return Ok(user);
            else
                return NotFound("User not found.");
        }

        [HttpGet]
        [Route("groups")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetGroups()
        {
            var groups = await _mediator.Send(new QueryAllGroupsCommand());
            return Ok(groups);
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetUsers()
        {
            var user = await _mediator.Send(new QueryAllUsersCommand());
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateNew([FromBody]CreateUserCommand command)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);
            try
            {
                int userId = await _mediator.Send(command);
                return Ok(userId);
            }
            catch (UserNameAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
