using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lax.Data.Entities.EntityFrameworkCore {

    public abstract class EntityFrameworkModelBuilder<TDbContext, TEntity> : IEntityFrameworkModelBuilder<TDbContext>
        where TDbContext : DbContext where TEntity : class {

        public void Build(ModelBuilder builder) => Build(builder.Entity<TEntity>());

        public abstract void Build(EntityTypeBuilder<TEntity> builder);

        public Type DbContextType => typeof(TDbContext);
        public Type EntityType => typeof(TEntity);

    }

}