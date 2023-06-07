using System;

namespace Lax.Business.Bus.Validation {

    [AttributeUsage(AttributeTargets.Class)]
    public class ValidateRequestAttribute : Attribute {

        public ValidateRequestAttribute() { }

    }

}