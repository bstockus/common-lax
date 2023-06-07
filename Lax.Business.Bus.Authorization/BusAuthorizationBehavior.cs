using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Lax.Business.Authorization.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Lax.Business.Bus.Authorization {

    public class BusAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        private readonly ILogger<BusAuthorizationBehavior<TRequest, TResponse>> _logger;
        private readonly IAuthorizationUserProvider _authorizationUserProvider;
        private readonly IAuthorizationService _authorizationService;

        public BusAuthorizationBehavior(
            ILogger<BusAuthorizationBehavior<TRequest, TResponse>> logger,
            IAuthorizationUserProvider authorizationUserProvider,
            IAuthorizationService authorizationService) {
            _logger = logger;
            _authorizationUserProvider = authorizationUserProvider;
            _authorizationService = authorizationService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {
            var customAttributes = typeof(TRequest).GetTypeInfo().GetCustomAttributes(false);

            if (customAttributes.All(ca => ca.GetType() != typeof(AuthorizeRequestAttribute))) {
                return await next();
            }

            var messageType = typeof(TRequest);

            var policyName =
                (customAttributes.FirstOrDefault(ca => ca.GetType() == typeof(AuthorizeRequestAttribute)) as
                    AuthorizeRequestAttribute)?.PolicyName;

            var currentUser = _authorizationUserProvider.GetCurrentUser();

            var result = await _authorizationService.AuthorizeAsync(currentUser, request, policyName);

            if (!result.Succeeded) {
                throw new BusUnauthorizedUserException(currentUser, messageType, policyName);
            }

            _logger.LogInformation("Authorized Message {MessageType} for {User} with {Policy}",
                messageType.FullName, currentUser.Identity.Name, policyName);
            return await next();
        }

    }

}