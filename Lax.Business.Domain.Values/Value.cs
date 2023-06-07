using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NReco.Linq;
using Value;

namespace Lax.Business.Domain.Values {

    // Represents an abstract class for a Value Type
    public abstract class Value<T> : ValueType<T> {

        private static readonly Lazy<Func<object, IEnumerable<object>>> AttributesToBeUsedForEqualityLazyFunc =
            new Lazy<Func<object, IEnumerable<object>>>(
                GenerateAttributesToBeUsedForEqualityFunc);

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality() =>
            AttributesToBeUsedForEqualityLazyFunc.Value(this);

        private static Func<object, IEnumerable<object>> GenerateAttributesToBeUsedForEqualityFunc() {
            var valueType = typeof(T);

            var lambdaParser = new LambdaParser();

            var expressionText = new StringBuilder("new [] {");

            var isFirstProperty = true;

            foreach (var propertyInfo in valueType.GetProperties()
                .Where(_ =>
                    _.CanRead && _.GetMethod.IsPublic &&
                    _.GetCustomAttributes(typeof(AtomicValueAttribute), true).Any())) {
                if (!isFirstProperty) {
                    expressionText.Append(", ");
                }

                isFirstProperty = false;

                expressionText.Append($"_.{propertyInfo.Name}");
            }

            expressionText.Append("}");

            var expression = expressionText.ToString();

            return value => (IEnumerable<object>) lambdaParser.Eval(expression, new Dictionary<string, object> {
                {"_", value}
            });
        }

    }

}