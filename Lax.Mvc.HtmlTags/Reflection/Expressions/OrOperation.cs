using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class OrOperation {

        public Expression<Func<T, bool>> GetPredicateBuilder<T>(Expression<Func<T, object>> leftPath, object leftValue,
            Expression<Func<T, object>> rightPath, object rightValue) {
            var comp = new ComposableOrOperation();
            comp.Set(leftPath, leftValue);
            comp.Set(rightPath, rightValue);
            return comp.GetPredicateBuilder<T>();
        }

        public Expression<Func<T, bool>> GetPredicateBuilder<T>(Expression<Func<T, object>> leftPath,
            IEnumerable<object> leftValue, Expression<Func<T, object>> rightPath, object rightValue) {
            var comp = new ComposableOrOperation();
            comp.Set(leftPath, leftValue);
            comp.Set(rightPath, rightValue);
            return comp.GetPredicateBuilder<T>();
        }

        public Expression<Func<T, bool>> GetPredicateBuilder<T>(Expression<Func<T, object>> leftPath, object leftValue,
            Expression<Func<T, object>> rightPath, IEnumerable<object> rightValue) {
            var comp = new ComposableOrOperation();
            comp.Set(leftPath, leftValue);
            comp.Set(rightPath, rightValue);
            return comp.GetPredicateBuilder<T>();
        }

    }

}