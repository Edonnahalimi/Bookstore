using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;

namespace BookstoreApi;

public static class AppExtensions
{
    public static IServiceCollection AddAuthenticationConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authority = configuration["IdentityServer:Authority"]
            ?? throw new InvalidOperationException(
                "IdentityServer authority is not configured.");

        services
            .AddAuthentication()
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.Authority = authority;

                    options.Audience = "bookstore";

                    options.RequireHttpsMetadata = true;

                    options.MapInboundClaims = false;
                });

        return services;
    }

    public static IServiceCollection AddAuthorizationConfiguration(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("BookCrud", policy =>
            {
                policy.AddAuthenticationSchemes(
                    JwtBearerDefaults.AuthenticationScheme);

                policy.RequireAuthenticatedUser();

                policy.RequireClaim(
                    "scope",
                    "bookstore");

                policy.RequireClaim(
                    "client_id",
                    "bookstore-client");
            });

            options.AddPolicy("BookSearch", policy =>
            {
                policy.AddAuthenticationSchemes(
                    JwtBearerDefaults.AuthenticationScheme);

                policy.RequireAuthenticatedUser();

                policy.RequireClaim(
                    "scope",
                    "bookstore");

                policy.RequireClaim(
                    "client_id",
                    "bookstore-search");
            });
        });

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authority = configuration["IdentityServer:Authority"]
            ?? throw new InvalidOperationException(
                "IdentityServer authority is not configured.");

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Bookstore API",
                    Version = "v1",
                    Description =
                        "Bookstore REST API with OAuth2 protection"
                });

            options.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,

                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(
                                $"{authority}/connect/authorize"),

                            Scopes = new Dictionary<string, string>
                            {
                                ["bookstore"] =
                                    "Bookstore API"
                            }
                        }
                    }
                });

            options.AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    [
                        new OpenApiSecuritySchemeReference(
                            "oauth2",
                            document)
                    ] = new List<string>
                    {
                        "bookstore"
                    }
                });
        });

        return services;
    }

    public static WebApplication UseSwaggerConfiguration(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Bookstore API v1");

                options.OAuthClientId(
                    "bookstore-search");
            });
        }

        return app;
    }
}