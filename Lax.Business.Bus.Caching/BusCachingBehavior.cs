using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Lax.Business.Bus.Caching {

    public class BusCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<BusCachingBehavior<TRequest, TResponse>> _logger;

        public BusCachingBehavior(
            IMemoryCache memoryCache,
            ILogger<BusCachingBehavior<TRequest, TResponse>> logger) {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {

            var customAttributes = typeof(TRequest).GetTypeInfo().GetCustomAttributes(true);

            if (customAttributes.All(ca => ca.GetType() != typeof(CacheRequestAttribute))) {
                return await next();
            }

            var customAttribute =
                    customAttributes.FirstOrDefault(ca => ca.GetType() == typeof(CacheRequestAttribute)) as
                        CacheRequestAttribute;

            var requestCacheKey = BusRequestCacheEntry.GenerateRequestCacheKey(request);

            if (_memoryCache.TryGetValue<BusRequestCacheEntry>(requestCacheKey, out var cacheEntry)) {
                _logger.LogInformation("Bus Cache Hit for {RequestCacheKey}", requestCacheKey);
                return await Task.FromResult(cacheEntry.Result is TResponse result ? result : default);
            }

            _logger.LogInformation("Bus Cache Miss for {RequestCacheKey}", requestCacheKey);

            var results = await next();
            Debug.Assert(customAttribute != null, nameof(customAttribute) + " != null");
            _memoryCache.Set(requestCacheKey, new BusRequestCacheEntry(requestCacheKey, results),
                customAttribute.CacheDuration ?? TimeSpan.FromMinutes(5));
            return results;

        }

    }

}