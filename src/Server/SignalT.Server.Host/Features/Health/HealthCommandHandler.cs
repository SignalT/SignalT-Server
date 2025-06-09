using MediatR;

namespace SignalT.Server.Host.Features.Health;

public class HealthCommandHandler : IRequestHandler<HealthCommand, CommandResult>
{
    public async Task<CommandResult> Handle(HealthCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        var s = "Api health " + DateTime.UtcNow;
        return new CommandResult
        {
            Data = s
        };
    }
}