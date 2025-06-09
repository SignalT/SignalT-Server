using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SignalT.Server.Host.Features.Health;

[ApiController]
[Route("api/v1/health")]

public class HealthController
{
    private readonly IMediator _mediator;

    public HealthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "Health")]
    public async Task<string> Get()
    {
        var result = await _mediator.Send(new HealthCommand());
        return result.Data.ToString()!;
    }
}