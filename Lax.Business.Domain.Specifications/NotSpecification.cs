namespace Lax.Business.Domain.Specifications {

    public class NotSpecification<T> : CompositeSpecification<T> {

        public ISpecification<T> Specification { get; }

        public NotSpecification(ISpecification<T> spec) => Specification = spec;

        public override bool IsSatisfiedBy(T o) => !Specification.IsSatisfiedBy(o);

    }

}