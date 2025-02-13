using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;

namespace UserManager.Application.Members.Commands
{
    public sealed class UpdateMemberCommand : IRequest<Member>
    {
        public int Id { get; set; }
        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }

        public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;

            public UpdateMemberCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Member> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
            {
                var existingMember = await _unitOfWork.MemberRepository.GetMemberById(request.Id)
                    ?? throw new InvalidOperationException("member not found");

                existingMember.Update(request.FistName,
                                      request.LastName,
                                      request.Gender,
                                      request.Email,
                                      request.IsActive);

                _unitOfWork.MemberRepository.UpdateMember(existingMember);
                await _unitOfWork.CommitAsync();

                return existingMember;
            }
        }
    }
}
