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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            var user = await _mediator.Send(new QueryUserCommand(id));
            if (user != null)
                return Ok(user);
            else
                return NotFound("User not found.");
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            if(await _mediator.Send(new DeleteUserCommand(id)))            
                return Ok();
            else
                return NotFound();
        }

        [HttpGet]
        [Route("find/{name}")]
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
                return NotFound();
        }

        [HttpGet]
        [Route("check/{name}")]
        public async Task<IActionResult> Check(string name)
        {
            if (string.IsNullOrEmpty(name)) return Ok(true);
            var user = await _mediator.Send(new QueryUserCommand(name));
            return Ok(user == null);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var user = await _mediator.Send(new QueryAllUsersCommand(string.Empty));
            return Ok(user);
        }

        [HttpGet]
        [Route("findany/{name}")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Find(string name)
        {
            var user = await _mediator.Send(new QueryAllUsersCommand(name));
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateNew([FromBody]CreateUserCommand command)
        {
            if (!this.ModelState.IsValid)
                return BadRequest(this.ModelState);
            try
            {
                int id = await _mediator.Send(command);
                var user = await _mediator.Send(new QueryUserCommand(id));
                return Ok(user);
            }
            catch (UserNameAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }           
        }
    }
}
