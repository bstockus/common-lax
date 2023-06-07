namespace Lax.Business.Domain.Specifications {

    public abstract class CompositeSpecification<T> : ISpecification<T> {

        public abstract bool IsSatisfiedBy(T o);

    }

}