using System.Security.Claims;

namespace Lax.Business.Authorization.Abstractions {

    public interface IAuthorizationUserProvider {

        ClaimsPrincipal GetCurrentUser();

    }

}