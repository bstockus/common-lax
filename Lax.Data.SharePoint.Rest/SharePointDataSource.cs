using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Lax.Data.SharePoint.Rest {

    public class SharePointDataSource<TEntity> : ISharePointDataSource<TEntity> where TEntity : Entity {

        private readonly ISharePointEntityContentTypeProvider _sharePointEntityContentTypeProvider;

        public SharePointDataSource(
            ISharePointEntityContentTypeProvider sharePointEntityContentTypeProvider) {
            _sharePointEntityContentTypeProvider = sharePointEntityContentTypeProvider;
        }

        public async Task<IEnumerable<TEntity>> LoadItems(NetworkCredential networkCredential) {
            
            var sharePointLoader = new SharePointLoader<TEntity>(
                _sharePointEntityContentTypeProvider.ContentTypeMap<TEntity>());

            return await sharePointLoader.FetchAll(networkCredential);

        }

        public async Task<TEntity> AddItem(NetworkCredential networkCredential, TEntity entity) {
            var sharePointLoader = new SharePointLoader<TEntity>(
                _sharePointEntityContentTypeProvider.ContentTypeMap<TEntity>());

            return await sharePointLoader.AddItem(networkCredential, entity);
        }

        public async Task UpdateItem(NetworkCredential networkCredential, TEntity entity,
            IEnumerable<Expression<Func<TEntity, object>>> fields = null) {

            var sharePointLoader = new SharePointLoader<TEntity>(
                _sharePointEntityContentTypeProvider.ContentTypeMap<TEntity>());

            await sharePointLoader.UpdateItem(networkCredential, entity, fields);
        }

        public async Task DeleteItem(
            NetworkCredential networkCredential,
            TEntity entity) {

            var sharePointLoader = new SharePointLoader<TEntity>(
                _sharePointEntityContentTypeProvider.ContentTypeMap<TEntity>());

            await sharePointLoader.DeleteItem(networkCredential, entity);
        }

    }

}