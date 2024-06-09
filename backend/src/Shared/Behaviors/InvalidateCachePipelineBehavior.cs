using Shared.Caching;

namespace Shared.Behaviors
{
    public class InvalidateCachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IInvalidateCacheCommand
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<InvalidateCachePipelineBehavior<TRequest, TResponse>> _logger;

        public InvalidateCachePipelineBehavior(ICacheService cacheService, ILogger<InvalidateCachePipelineBehavior<TRequest, TResponse>> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Invalidated cache with keys: {Keys}", request.Keys);
            
            foreach (var key in request.Keys)
                _cacheService.DeleteAsync(key, cancellationToken);

            return next();
        }
    }
}
