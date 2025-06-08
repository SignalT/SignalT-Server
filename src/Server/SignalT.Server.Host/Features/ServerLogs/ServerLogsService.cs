using Microsoft.AspNetCore.SignalR;

namespace SignalT.Server.Host.Features.ServerLogs;

public class ServerLogsService : IServerLogsService
{
    private readonly IHubContext<ListHub> _hub;
    private List<string> _logs;

    public ServerLogsService(IHubContext<ListHub> hub)
    {
        _logs = new List<string>();
        _hub = hub;
    }

    public async Task Add(string item)
    {
        _logs.Add(item);
        await _hub.Clients.All.SendAsync("NewItemAdded", item);
    }

    public List<string> Get()
    {
       return _logs;
    }
}