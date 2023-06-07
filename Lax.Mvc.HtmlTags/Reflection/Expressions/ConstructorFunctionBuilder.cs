using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class ConstructorFunctionBuilder<T> {

        public Func<IArguments, T> CreateBuilder(ConstructorInfo constructor) {
            var args = Expression.Parameter(typeof(IArguments), "x");


            var arguments =
                constructor.GetParameters().Select(
                    param => ToParameterValueGetter(args, param.ParameterType, param.Name));

            var ctorCall = Expression.New(constructor, arguments);

            var lambda = Expression.Lambda(typeof(Func<IArguments, T>), ctorCall, args);
            return (Func<IArguments, T>) lambda.Compile();
        }

        public static Expression ToParameterValueGetter(ParameterExpression args, Type type, string argName) {
            var method = typeof(IArguments).GetMethod("Get")?.MakeGenericMethod(type);
            return Expression.Call(args, method ?? throw new InvalidOperationException(), Expression.Constant(argName));
        }

    }

}