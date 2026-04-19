using Microsoft.Extensions.DependencyInjection;

namespace Cratebase.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCratebaseApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services;
    }
}
