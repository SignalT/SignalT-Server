using Microsoft.AspNetCore.SignalR;
using SignalT.Server.Host.Features.ServerLogs;

namespace SignalT.Server.Host;

public class YourBackgroundService : BackgroundService
{
    private readonly IServerLogsService _serverLogsService;

    public YourBackgroundService(IServerLogsService serverLogsService)
    {
        _serverLogsService = serverLogsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var newItem = "Ny rad " + DateTime.Now;

            // Lägg till i din lista
            // YourSharedList.Instance.Add(newItem);

            // Push till klienter via SignalR
            await _serverLogsService.Add(newItem);

            await Task.Delay(2000, stoppingToken);
        }
    }
}