using UserManager.Domain.Entities;

namespace UserManager.Domain.Interfaces
{
    public interface IMemberDapperRepository
    {
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMemberById(int id);
    }
}
