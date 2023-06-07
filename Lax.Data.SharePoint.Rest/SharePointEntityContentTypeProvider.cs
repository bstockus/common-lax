using System;
using System.Collections.Generic;

namespace Lax.Data.SharePoint.Rest {

    public class SharePointEntityContentTypeProvider : ISharePointEntityContentTypeProvider {

        private readonly IDictionary<Type, Type> _contentTypeMaps;

        public SharePointEntityContentTypeProvider(
            IDictionary<Type, Type> contentTypeMaps) {

            _contentTypeMaps = contentTypeMaps;
        }

        public EntityContentTypeMap<TEntity> ContentTypeMap<TEntity>() where TEntity : Entity {
            if (!_contentTypeMaps.ContainsKey(typeof(TEntity))) {
                return null;
            }

            var contentTypeMap = _contentTypeMaps[typeof(TEntity)];

            return Activator.CreateInstance(contentTypeMap) as EntityContentTypeMap<TEntity>;

        }

    }

}