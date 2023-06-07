namespace Lax.Business.Domain.Specifications {

    public interface ISpecification<in T> {

        bool IsSatisfiedBy(T o);

    }

}