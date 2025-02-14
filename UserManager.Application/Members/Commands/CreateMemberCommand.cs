﻿using MediatR;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;

namespace UserManager.Application.Members.Commands
{
    public class CreateMemberCommand : MemberCommandBase
    {

        public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Member>
        {
            private readonly IUnitOfWork _unitOfWork;

            public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
            {
                var newMember = new Member(request.FistName, request.LastName, request.Gender, request.Email, request.IsActive);

                await _unitOfWork.MemberRepository.AddMember(newMember);
                await _unitOfWork.CommitAsync();

                return newMember;
            }
        }
    }
}
