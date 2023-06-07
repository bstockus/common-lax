using System.Security.Principal;
using System.Threading.Tasks;
using Lax.Cli.Abstractions;

namespace Lax.Business.Authorization.Static {

    public class StaticAuthorizationCliDecorator : ICliDecorator {

        private readonly StaticAuthorizationUserProvider _staticAuthorizationUserProvider;

        public string DecoratorName => "SID";

        public StaticAuthorizationCliDecorator(StaticAuthorizationUserProvider staticAuthorizationUserProvider) =>
            _staticAuthorizationUserProvider = staticAuthorizationUserProvider;

        public async Task Run(string input) => await Task.Run(() => _staticAuthorizationUserProvider.UpdateOptions(
            input.ToUpper().Equals("WIN")
                ? WindowsIdentity.GetCurrent().User?.Value
                : input));

    }

}