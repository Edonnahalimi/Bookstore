using Microsoft.Extensions.DependencyInjection;

namespace BookstoreApi.Domain;

public static class DomainConfig
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        return services;
    }
}