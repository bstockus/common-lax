﻿using System;

namespace Lax.Business.Domain.Specifications {

    public class ExpressionSpecification<T> : CompositeSpecification<T> {

        private readonly Func<T, bool> _expression;

        public ExpressionSpecification(Func<T, bool> expression) =>
            _expression = expression ?? throw new ArgumentNullException();

        public override bool IsSatisfiedBy(T o) => _expression(o);

    }

}