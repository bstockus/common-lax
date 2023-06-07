using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lax.Helpers.Common;

namespace Lax.Mvc.HtmlTags.Reflection {

    public class AccessorRules {

        private readonly Cache<Type, Cache<IAccessor, IList<object>>> _rules =
            new Cache<Type, Cache<IAccessor, IList<object>>>(
                type => new Cache<IAccessor, IList<object>>(a => new List<object>()));

        public void Add(IAccessor accessor, object rule)
            => _rules[accessor.OwnerType][accessor].Fill(rule);

        public void Add<T>(Expression<Func<T, object>> expression, object rule)
            => Add(expression.ToAccessor(), rule);

        public void Add<T, TRule>(Expression<Func<T, object>> expression) where TRule : new()
            => Add(expression, new TRule());

        public IEnumerable<T> AllRulesFor<T>(IAccessor accessor) => _rules[accessor.OwnerType][accessor].OfType<T>();

        public T FirstRule<T>(IAccessor accessor) => AllRulesFor<T>(accessor).FirstOrDefault();

        public IEnumerable<TRule> AllRulesFor<T, TRule>(Expression<Func<T, object>> expression) =>
            AllRulesFor<TRule>(expression.ToAccessor());

        public TRule FirstRule<T, TRule>(Expression<Func<T, object>> expression) =>
            FirstRule<TRule>(expression.ToAccessor());

    }

}