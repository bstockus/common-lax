using System;
using Microsoft.EntityFrameworkCore;

namespace Lax.Data.Entities.EntityFrameworkCore {

    public interface IEntityFrameworkModelBuilder {

        Type DbContextType { get; }
        Type EntityType { get; }

    }

    public interface IEntityFrameworkModelBuilder<TDbContext> : IEntityFrameworkModelBuilder
        where TDbContext : DbContext {

        void Build(ModelBuilder builder);

    }

}