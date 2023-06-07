using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Lax.Data.SharePoint.Rest.FieldValues;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public static class FieldConverterRegistry {

        private static readonly Dictionary<Type, IFieldConverter> FieldConverters =
            new() {
                {typeof(string), new StringFieldConverter()},
                {typeof(int?), new NullableIntegerFieldConverter()},
                {typeof(double?), new DoubleFieldConverter()},
                {typeof(decimal?), new DecimalFieldConverter()},
                {typeof(bool?), new BooleanFieldConverter()},
                {typeof(DateTime?), new DateTimeFieldConverter()},
                {typeof(string[]), new StringArrayFieldConverter()},
                {typeof(int[]), new IntegerArrayFieldConverter()},
                {typeof(TaxonomyFieldValue), new TaxonomyFieldConverter()},
                {typeof(TaxonomyFieldValue[]), new TaxonomyArrayFieldConverter()},
                {typeof(PersonFieldValue), new PersonFieldConverter()},
                {typeof(PersonFieldValue[]), new PersonArrayFieldConverter()},
                {typeof(LookupFieldValue), new LookupFieldConverter()}
            };

        public static IFieldConverter GetFieldConverterForMember(LambdaExpression lambdaExpression) {
            var memberInfo = LambdaHelpers.GetMemberInfo(lambdaExpression);

            var propertyInfo = memberInfo as PropertyInfo;

            Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");
            return FieldConverters[propertyInfo.PropertyType];
        }

        

    }

}