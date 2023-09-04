using OnlineEvent.Abstract.UnitOfWorks;

namespace OnlineEvent.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
