using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lax.Data.Entities.EntityFrameworkCore {

    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext {

        private readonly TDbContext _context;

        public UnitOfWork(
            TDbContext context) =>
            _context = context;

        public async Task SaveChanges(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class =>
            _context.Set<TEntity>();

    }

}