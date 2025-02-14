using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManager.Application.Members.Commands;
using UserManager.Application.Members.Queries;
using UserManager.Domain.Interfaces;

namespace UserManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string memberNotFound = "Member not found.";

        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var query = new GetMembersQuery();
            var members = await _mediator.Send(query);
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var query = new GetMemberByIdQuery { Id = id};
            var member = await _mediator.Send(query);
            
            return member != null ? Ok(member) : NotFound(memberNotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberCommand command)
        {
            var createdMember = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetMember), new { id = createdMember.Id }, createdMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, UpdateMemberCommand command)
        {
            command.Id = id;
            var updateMember = await _mediator.Send(command);

            return updateMember != null ? Ok(updateMember) : NotFound(memberNotFound);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var command = new DeleteMemberCommand { Id = id };
            var deletedMember = await _mediator.Send(command);

            return deletedMember != null ? Ok(deletedMember) : NotFound(memberNotFound);
        }
    }
}
