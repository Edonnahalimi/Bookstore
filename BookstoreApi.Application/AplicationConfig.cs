using BookstoreApi.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookstoreApi.Application;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplication(
       this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(ApplicationConfig).Assembly);

            cfg.AddOpenBehavior(
                typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(
            typeof(ApplicationConfig).Assembly);

        return services;
    }
}
