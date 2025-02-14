using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;

namespace UserManager.Application.Members.Queries
{
    public class GetMembersQuery : IRequest<IEnumerable<Member>>
    {
        public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<Member>>
        {
            private readonly IMemberDapperRepository _memberDapperRepository;

            public GetMembersQueryHandler(IMemberDapperRepository memberDapperRepository)
            {
                this._memberDapperRepository = memberDapperRepository;
            }

            public async Task<IEnumerable<Member>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
            {
                var members = await _memberDapperRepository.GetMembers();
                return members;
            }
        }
    }
}
