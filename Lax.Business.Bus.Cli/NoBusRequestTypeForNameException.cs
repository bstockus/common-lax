using System;

namespace Lax.Business.Bus.Cli {

    public class NoBusRequestTypeForNameException : Exception {

        public NoBusRequestTypeForNameException(string name) : base($"No Bus Request Type Found for {name}") { }

    }

}