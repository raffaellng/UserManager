using UserManager.Domain.Interfaces;
using UserManager.Infrastructure.Context;

namespace UserManager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IMemberRepository? _memberRepository;
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IMemberRepository MemberRepository
        {
            get
            {
                return _memberRepository = _memberRepository ?? new MemberRepository(_appDbContext);

            }
        }

        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
