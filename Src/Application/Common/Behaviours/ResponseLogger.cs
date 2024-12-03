using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;
public class ResponseLogger<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    public ResponseLogger(ILogger<TResponse> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }
    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.LogInformation("---Post BlogApp Processing: {Name} {@UserId}",
            name, _currentUserService.UserId);
        return Task.CompletedTask;
    }
}