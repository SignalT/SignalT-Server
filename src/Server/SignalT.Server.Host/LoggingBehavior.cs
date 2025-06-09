using System.Diagnostics;
using MediatR;
using SignalT.Server.Host.Features.ServerLogs;

namespace SignalT.Server.Host;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IServerLogsService _serverLogsService;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IServerLogsService serverLogsService)
    {
        _logger = logger;
        _serverLogsService = serverLogsService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var time = new Stopwatch();
        time.Start();
        var correlationId = Guid.NewGuid();
        _logger.LogInformation($"Handling {typeof(TRequest).Name} Start id " + correlationId);
        await _serverLogsService.Add($"Calling {typeof(TRequest).Name} Id:{correlationId}");
        var response = await next();
        time.Stop();
        if (time.Elapsed.Seconds > 2)
        {
            _logger.LogError($"Handled {typeof(TRequest).Name} runningTime: {time.Elapsed.ToString("g")}");
        }
        else
        {
            _logger.LogInformation($"Handled {typeof(TRequest).Name} runningTime: {time.Elapsed.ToString("g")}");
        }

        await _serverLogsService.Add($"{typeof(TRequest).Name} Id:{correlationId} Done after: {time.Elapsed.Milliseconds} ms");
        return response;
    }
}