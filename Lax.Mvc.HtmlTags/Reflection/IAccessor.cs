using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection {

    public interface IAccessor {

        Type PropertyType { get; }
        PropertyInfo InnerProperty { get; }
        Type DeclaringType { get; }
        string Name { get; }
        Type OwnerType { get; }
        void SetValue(object target, object propertyValue);
        object GetValue(object target);

        IAccessor GetChildAccessor<T>(Expression<Func<T, object>> expression);

        string[] PropertyNames { get; }

        Expression<Func<T, object>> ToExpression<T>();

        IAccessor Prepend(PropertyInfo property);

        IEnumerable<IValueGetter> Getters();

    }

}