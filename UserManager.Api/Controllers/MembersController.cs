using Microsoft.AspNetCore.Mvc;
using UserManager.Domain.Interfaces;

namespace UserManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _unitOfWork.MemberRepository.GetAll();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberById(id);
            return member != null ? Ok(member) : NotFound("Member not found.");
        }
    }
}
