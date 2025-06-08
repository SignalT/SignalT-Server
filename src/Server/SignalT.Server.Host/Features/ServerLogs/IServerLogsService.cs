namespace SignalT.Server.Host.Features.ServerLogs;

public interface IServerLogsService
{
    Task Add(string item);

    List<string> Get();
}