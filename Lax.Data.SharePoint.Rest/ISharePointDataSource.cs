using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Lax.Data.SharePoint.Rest {

    public interface ISharePointDataSource<TEntity> where TEntity : Entity {

        Task<IEnumerable<TEntity>> LoadItems(NetworkCredential networkCredential);

        Task<TEntity> AddItem(NetworkCredential networkCredential, TEntity entity);

        Task DeleteItem(NetworkCredential networkCredential, TEntity entity);

        Task UpdateItem(NetworkCredential networkCredential, TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> fields = null);

    }

}
