using System.Collections.Generic;
using System.Security.Claims;
using Lax.Business.Authorization.Abstractions;

namespace Lax.Business.Authorization.Static {

    public class StaticAuthorizationUserProvider : IAuthorizationUserProvider {

        private string _windowsSid;


        public StaticAuthorizationUserProvider(
            string windowsSid) =>
            _windowsSid = windowsSid;

        public ClaimsPrincipal GetCurrentUser() {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.PrimarySid, _windowsSid)
            };

            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }

        public void UpdateOptions(string newWindowsSid) => _windowsSid = newWindowsSid;

    }

}