using System;
using System.Linq.Expressions;

namespace Lax.Data.SharePoint.Rest {

    public class FieldBuilder<TEntity> where TEntity : Entity {

        private readonly Expression<Func<TEntity, object>> _propertyExpression;
        private string _entityPropertyName;
        private string _internalName;
        private string _typeName = "";


        public FieldBuilder(Expression<Func<TEntity, object>> propertyExpression) {
            _propertyExpression = propertyExpression;
        }

        public FieldBuilder<TEntity> EntityPropertyName(string entityPropertyName) {
            _entityPropertyName = entityPropertyName;
            return this;
        }

        public FieldBuilder<TEntity> InternalName(string internalName) {
            _internalName = internalName;
            return this;
        }

        public FieldBuilder<TEntity> TypeAsString(string typeName) {
            _typeName = typeName;
            return this;
        }

        public FieldInfo<TEntity> AsFieldInfo() =>
            new(
                _propertyExpression,
                _entityPropertyName,
                _internalName,
                _typeName);

    }

}