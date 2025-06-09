using MediatR;

namespace SignalT.Server.Host.Features.Health;

public class HealthCommand : IRequest<CommandResult>
{
}