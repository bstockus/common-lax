using System;

namespace Lax.Business.Bus.UnitOfWork {

    [AttributeUsage(AttributeTargets.Class)]
    public class RequestUnitOfWorkAttribute : Attribute {

        public Type DbContextType { get; }

        public RequestUnitOfWorkAttribute(Type dbContextType) => DbContextType = dbContextType;

    }

}