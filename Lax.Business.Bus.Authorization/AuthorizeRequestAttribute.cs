using System;

namespace Lax.Business.Bus.Authorization {

    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeRequestAttribute : Attribute {

        public string PolicyName { get; }

        public AuthorizeRequestAttribute(string policyName) => PolicyName = policyName;

    }

}