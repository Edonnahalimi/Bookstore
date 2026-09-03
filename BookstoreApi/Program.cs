using BookstoreApi;
using BookstoreApi.Application;
using BookstoreApi.Domain;
using BookstoreApi.Identity;
using BookstoreApi.Infrastructure;
using BookstoreApi.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomain();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services
    .AddIdentityServer(options =>
    {
        options.UserInteraction.LoginUrl = "/Account/Login";
        options.UserInteraction.LoginReturnUrlParameter = "returnUrl";
    })
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddTestUsers(Config.Users);

builder.Services.AddAuthenticationConfiguration(
    builder.Configuration);

builder.Services.AddAuthorizationConfiguration();

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerConfiguration(
    builder.Configuration);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapDefaultControllerRoute();
app.Run();