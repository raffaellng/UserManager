using Microsoft.EntityFrameworkCore;
using UserManager.Domain.Entities;
using UserManager.Domain.Interfaces;
using UserManager.Infrastructure.Context;

namespace UserManager.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Member> AddMember(Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            await _context.Members.AddAsync(member);
            return member;
        }

        public async Task<Member> DeleteMember(int id)
        {
            var member = await GetMemberById(id);

            if (member is null)
                throw new InvalidOperationException("Member not found");

            _context.Members.Remove(member);
            return member;
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            var memberList = await _context.Members.ToListAsync();
            return memberList ?? Enumerable.Empty<Member>();
        }

        public async Task<Member> GetMemberById(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member is null)
                throw new InvalidOperationException("Member not found");

            return member;
        }

        public void UpdateMember(Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            _context.Members.Update(member);
        }
    }
}
