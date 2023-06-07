using System;
using System.Collections.Generic;
using System.Reflection;
using Lax.Helpers.Common;

namespace Lax.Mvc.HtmlTags.Conventions.Formatting {

    public class DisplayConversionRegistry {

        private readonly IList<StringifierStrategy> _strategies = new List<StringifierStrategy>();


        public Stringifier BuildStringifier() {
            var stringifier = new Stringifier();
            Configure(stringifier);
            return stringifier;
        }

        public void Configure(Stringifier stringifier) => _strategies.Each(stringifier.AddStrategy);


        private MakeDisplayExpression MakeDisplay(Func<GetStringRequest, bool> filter) =>
            new MakeDisplayExpression(func => {
                _strategies.Add(new StringifierStrategy {
                    Matches = filter,
                    StringFunction = func
                });
            });

        private MakeDisplayExpression<T> MakeDisplay<T>(Func<GetStringRequest, bool> filter) =>
            new MakeDisplayExpression<T>(func => {
                _strategies.Add(new StringifierStrategy {
                    Matches = filter,
                    StringFunction = func
                });
            });

        public MakeDisplayExpression IfTypeMatches(Func<Type, bool> filter) =>
            MakeDisplay(request => filter(request.PropertyType));

        public MakeDisplayExpression<T> IfIsType<T>() => MakeDisplay<T>(request => request.PropertyType == typeof(T));

        public MakeDisplayExpression<T> IfCanBeCastToType<T>() => MakeDisplay<T>(t => t.PropertyType.CanBeCastTo<T>());

        public MakeDisplayExpression IfPropertyMatches(Func<PropertyInfo, bool> matches) =>
            MakeDisplay(request => request.Property != null && matches(request.Property));

        public MakeDisplayExpression<T> IfPropertyMatches<T>(Func<PropertyInfo, bool> matches) =>
            MakeDisplay<T>(
                request =>
                    request.Property != null && request.PropertyType == typeof(T) && matches(request.Property));

        public class MakeDisplayExpression : MakeDisplayExpressionBase {

            public MakeDisplayExpression(Action<Func<GetStringRequest, string>> callback)
                : base(callback) { }

            public void ConvertBy(Func<GetStringRequest, string> display) => Callback(display);

            public void ConvertWith<TService>(Func<TService, GetStringRequest, string> display) =>
                Apply(o => display(o.Get<TService>(), o));

        }

        public class MakeDisplayExpression<T> : MakeDisplayExpressionBase {

            public MakeDisplayExpression(Action<Func<GetStringRequest, string>> callback)
                : base(callback) { }

            public void ConvertBy(Func<T, string> display) => Apply(o => display((T) o.RawValue));

            public void ConvertBy(Func<GetStringRequest, T, string> display) => Apply(o => display(o, (T) o.RawValue));

            public void ConvertWith<TService>(Func<TService, T, string> display) =>
                Apply(o => display(o.Get<TService>(), (T) o.RawValue));

        }

        public abstract class MakeDisplayExpressionBase {

            protected Action<Func<GetStringRequest, string>> Callback;

            public MakeDisplayExpressionBase(Action<Func<GetStringRequest, string>> callback) => Callback = callback;

            protected void Apply(Func<GetStringRequest, string> func) => Callback(func);

        }

    }

}