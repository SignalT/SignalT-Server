namespace SignalT.Server.Host;

public class CommandResult
{
    public CommandResult()
    {
        Errors = new List<CommandResultError>();
    }

    public object Data { get; set; } = null!;

    public List<CommandResultError> Errors { get; set; }

    public bool HasErrors => Errors.Any();
}