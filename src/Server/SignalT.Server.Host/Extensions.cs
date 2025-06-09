using MediatR;
using SignalT.Server.Host.Features.Health;

namespace SignalT.Server.Host;

public static class Extensions
{
    public static IServiceCollection AddServer(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(HealthCommandHandler).Assembly); });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}