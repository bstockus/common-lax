using System;
using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public static class ConstructorBuilder {

        public static LambdaExpression CreateSingleStringArgumentConstructor(Type concreteType) {
            var constructor = concreteType.GetConstructor(new Type[] {typeof(string)});
            if (constructor == null) {
                throw new ArgumentOutOfRangeException(nameof(concreteType), concreteType,
                    "Only types with a ctor(string) can be used here");
            }

            var argument = Expression.Parameter(typeof(string), "x");

            var ctorCall = Expression.New(constructor, argument);

            var funcType = typeof(Func<,>).MakeGenericType(typeof(string), concreteType);
            return Expression.Lambda(funcType, ctorCall, argument);
        }

    }

}