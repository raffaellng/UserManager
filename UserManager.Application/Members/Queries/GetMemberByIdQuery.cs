using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;

namespace UserManager.Application.Members.Queries
{
    public class GetMemberByIdQuery : IRequest<Member>
    {
        public int Id { get; set; }
        public class GetMemberByIdQueryHandle : IRequestHandler<GetMemberByIdQuery, Member>
        {
            private readonly IMemberDapperRepository _memberDapperRepository;

            public GetMemberByIdQueryHandle(IMemberDapperRepository memberDapperRepository)
            {
                _memberDapperRepository = memberDapperRepository;
            }

            public Task<Member> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
            {
                var member = _memberDapperRepository.GetMemberById(request.Id);
                return member;
            }
        }
    }
}
