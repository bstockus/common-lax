using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Lax.Helpers.Common;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class CollectionContainsPropertyOperation : IPropertyOperation {

        private const string Description = "contains";

        private readonly MethodInfo _method =
            typeof(Enumerable).GetMethods(
                BindingFlags.Static | BindingFlags.Public).First(m => m.Name.EqualsIgnoreCase("Contains"));

        public string OperationName => "Contains";

        public string Text => Description;

        public Func<object, Expression<Func<T, bool>>> GetPredicateBuilder<T>(MemberExpression propertyPath) =>
            valuesToCheck => {
                var enumerationOfObjects = (IEnumerable<object>) valuesToCheck;
                if (enumerationOfObjects == null) {
                    return c => false;
                }

                //what's the type of the collection?
                var valuesToCheckType = valuesToCheck.GetType();
                var collectionOf = valuesToCheckType.IsAnEnumerationOf();


                //capture and close the Enumerbable.Contains _method
                var closedMethod = _method.MakeGenericMethod(collectionOf);

                //the list that we need to call contains on
                var list = Expression.Constant(enumerationOfObjects);


                //lambda parameter
                var param = Expression.Parameter(typeof(T));

                //this should be a property call
                var memberAccess = Expression.MakeMemberAccess(param, propertyPath.Member);


                //call 'Contains' with the desired 'value' to check on the 'list'
                var call = Expression.Call(closedMethod, list, memberAccess);


                var lambda = Expression.Lambda<Func<T, bool>>(call, param);
                //return enumerationOfObjects.Contains(((PropertyInfo) propertyPath.Member).GetValue(c, null));
                return lambda;
            };

    }

}