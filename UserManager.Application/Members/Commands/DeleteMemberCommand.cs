using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;

namespace UserManager.Application.Members.Commands
{
    public class DeleteMemberCommand : IRequest<Member>
    {
        public int Id { get; set; }

        public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;

            public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Member> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
            {
                var deletedMember = await _unitOfWork.MemberRepository.DeleteMember(request.Id)
                    ?? throw new InvalidOperationException("member not found");
                await _unitOfWork.CommitAsync();
                return deletedMember;
            }
        }
    }
}
