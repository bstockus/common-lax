using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Lax.Business.Bus.Validation {

    public class BusValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        private readonly IServiceProvider _serviceProvider;

        public BusValidationBehavior(
            IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {
            var customAttributes = typeof(TRequest).GetTypeInfo().GetCustomAttributes(false);

            if (!customAttributes.Any(ca =>
                ca.GetType() == typeof(ValidateRequestAttribute) ||
                ca.GetType() == typeof(ValidateRequestAsyncAttribute))) {
                return await next();
            }

            var validators =
                _serviceProvider.GetService(typeof(IEnumerable<IValidator<TRequest>>)) as
                    IEnumerable<IValidator<TRequest>>;

            var validator = (validators ?? throw new InvalidOperationException()).FirstOrDefault();

            if (customAttributes.Any(ca => ca.GetType() == typeof(ValidateRequestAttribute))) {
                await validator.ValidateAndThrowAsync(request, cancellationToken);
            } else {
                await validator.ValidateAndThrowAsync(request, cancellationToken);
            }

            return await next();
        }

    }

}