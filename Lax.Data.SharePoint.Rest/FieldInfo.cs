using System;
using System.Linq.Expressions;

namespace Lax.Data.SharePoint.Rest {

    public class FieldInfo<TEntity> where TEntity : Entity {

        public Expression<Func<TEntity, object>> PropertyExpression { get; }
        public string EntityPropertyName { get; }
        public string InternalName { get; }
        public string TypeName { get; }

        public FieldInfo(
            Expression<Func<TEntity, object>> propertyExpression, 
            string entityPropertyName, 
            string internalName, 
            string typeName) {
            
            PropertyExpression = propertyExpression;
            EntityPropertyName = entityPropertyName;
            InternalName = internalName;
            TypeName = typeName;
        }

        

    }

}