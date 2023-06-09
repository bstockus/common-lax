﻿using System;
using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public interface IPropertyOperation {

        string OperationName { get; }
        string Text { get; }
        Func<object, Expression<Func<T, bool>>> GetPredicateBuilder<T>(MemberExpression propertyPath);

    }

}