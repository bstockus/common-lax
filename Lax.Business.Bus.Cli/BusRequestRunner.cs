using System;
using Lax.Serialization.Yaml;
using MediatR;

namespace Lax.Business.Bus.Cli {

    public class BusRequestRunner<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        public void Run(TRequest request, IMediator mediator, IYamlSerializer yamlSerializer) {
            var result = mediator.Send(request).GetAwaiter().GetResult();

            Console.WriteLine(
                "====== [RESULTS] ============================================================================");
            Console.WriteLine(yamlSerializer.Serialize(result));
        }

    }

    public class BusRequestRunner<TRequest> where TRequest : IRequest {

        public void Run(TRequest request, IMediator mediator) => mediator.Send(request).GetAwaiter().GetResult();

    }

}