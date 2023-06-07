using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Lax.Data.Entities.EntityFrameworkCore;
using MediatR;

namespace Lax.Business.Bus.UnitOfWork {

    public class BusUnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        private readonly IServiceProvider _serviceProvider;

        public BusUnitOfWorkBehavior(
            IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {
            var customAttribute = typeof(TRequest).GetTypeInfo().GetCustomAttributes<RequestUnitOfWorkAttribute>(true);

            var unitOfWorks = customAttribute.Select(unitOfWorkAttribute =>
                _serviceProvider.GetService(typeof(IUnitOfWork<>).MakeGenericType(unitOfWorkAttribute.DbContextType)) as
                    IUnitOfWork).ToList();

            var response = await next();

            foreach (var unitOfWork in unitOfWorks) {
                await unitOfWork.SaveChanges(cancellationToken);
            }

            return response;
        }

    }

}