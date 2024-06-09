using Shared.CQRS;

namespace Shared.Caching
{
    public interface IInvalidateCacheCommand
    {
        string[] Keys { get; }
    }

    public interface IInvalidateCacheCommand<TResponse> : ICommand<TResponse>, IInvalidateCacheCommand;
}
