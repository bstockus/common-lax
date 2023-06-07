using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lax.Data.Entities.EntityFrameworkCore {

    public interface IUnitOfWork {

        Task SaveChanges(CancellationToken cancellationToken = default);

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

    }

    public interface IUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext { }

}