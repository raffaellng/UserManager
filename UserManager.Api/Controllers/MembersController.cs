using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManager.Application.Members.Commands;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UserManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        //TEMP
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMediator _mediator;
        private const string memberNotFound = "Member not found.";

        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _unitOfWork.MemberRepository.GetAll();
            return Ok(members);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberById(id);
            return member != null ? Ok(member) : NotFound(memberNotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberCommand command)
        {
            var createdMember = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetMember), new { id = createdMember.Id }, createdMember);

        }

        [HttpPut("{Id}")]
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
