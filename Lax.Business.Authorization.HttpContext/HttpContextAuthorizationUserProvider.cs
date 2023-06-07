using System.Security.Claims;
using Lax.Business.Authorization.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Lax.Business.Authorization.HttpContext {

    public class HttpContextAuthorizationUserProvider : IAuthorizationUserProvider {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAuthorizationUserProvider(
            IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public ClaimsPrincipal GetCurrentUser() => _httpContextAccessor.HttpContext.User;

    }

}