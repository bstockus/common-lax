using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lax.Data.SharePoint.Rest {

    public class EntityContentTypeMap<TEntity> where TEntity : Entity {

        public List<FieldBuilder<TEntity>> FieldBuilders { get; } = new();

        public Guid ListGuid { get; private set; } = Guid.Empty;
        public string SiteUrl { get; private set; } = "";

        public void List(string siteUrl, Guid listGuid) {

            Field(_ => _.Id).EntityPropertyName("ID").InternalName("ID");
            Field(_ => _.Title).EntityPropertyName("Title").InternalName("Title");

            ListGuid = listGuid;
            SiteUrl = siteUrl;
        }
        
        public FieldBuilder<TEntity> Field(Expression<Func<TEntity, object>> propertyExpression) {

            var fieldBuilder = new FieldBuilder<TEntity>(propertyExpression);

            FieldBuilders.Add(fieldBuilder);

            return fieldBuilder;
        }

    }

}