using System;

namespace Lax.Business.Domain.Abstractions.Exceptions {

    public class DomainException : Exception {

        public DomainException(Type aggregateType, Guid aggregateId, string message) :
            base($"{aggregateType.Name}/{aggregateId} => {message}") { }

    }

}