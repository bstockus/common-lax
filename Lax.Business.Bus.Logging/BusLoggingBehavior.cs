using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;

namespace Lax.Business.Bus.Logging {

    public class BusLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        private readonly ILogger<BusLoggingBehavior<TRequest, TResponse>> _logger;

        public BusLoggingBehavior(
            ILogger<BusLoggingBehavior<TRequest, TResponse>> logger) =>
            _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var customAttributes = typeof(TRequest).GetTypeInfo().GetCustomAttributes(true);

            var response = await next();

            stopWatch.Stop();

            if (customAttributes.Any(ca => ca.GetType() == typeof(LogRequestAttribute))) {
                _logger.LogInformation("Handled Command {CommandType} in {Time}ms", typeof(TRequest).FullName,
                    stopWatch.ElapsedMilliseconds);
            }

            return response;
        }

    }

}