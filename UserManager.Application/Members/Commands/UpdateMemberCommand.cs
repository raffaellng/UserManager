using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;

namespace UserManager.Application.Members.Commands
{
    public sealed class UpdateMemberCommand : MemberCommandBase
    {
        public int Id { get; set; }

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

                existingMember.Update(request.FirstName,
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
