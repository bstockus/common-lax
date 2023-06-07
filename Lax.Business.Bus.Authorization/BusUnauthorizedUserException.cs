using System;
using System.Security.Claims;

namespace Lax.Business.Bus.Authorization {

    public class BusUnauthorizedUserException : Exception {

        public ClaimsPrincipal User { get; }
        public Type MessageType { get; }
        public string Policy { get; }

        public BusUnauthorizedUserException(ClaimsPrincipal user, Type messageType, string policy) : base(
            (string)
            $"Unauthorized User: {user.Identity.Name}, MessageType: {messageType.FullName}, Policy: {policy}") {
            User = user;
            MessageType = messageType;
            Policy = policy;
        }

    }

}