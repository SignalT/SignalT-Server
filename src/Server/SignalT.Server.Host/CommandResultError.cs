namespace SignalT.Server.Host;

public class CommandResultError
{
    public int ErrorCode { get; set; }

    public string Message { get; set; } = string.Empty;
}