using System;

namespace Lax.Business.Bus.Logging {

    [AttributeUsage(AttributeTargets.Class)]
    public class LogRequestAttribute : Attribute {

        public LogRequestAttribute() { }

    }

}