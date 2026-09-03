using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace BookstoreApi.Identity;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource(
                "bookstore",
                "Bookstore API")
            {
                Scopes =
                {
                    "bookstore"
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope(
                "bookstore",
                "Bookstore API")
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "bookstore-client",

                AllowedGrantTypes =
                    GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret(
                        "bookstore-secret".Sha256())
                },

                AllowedScopes =
                {
                    "bookstore"
                }
            },

            new Client
            {
                ClientId = "bookstore-search",

                AllowedGrantTypes =
                    GrantTypes.Implicit,

                RedirectUris =
                {
                    "https://localhost:7143/swagger/oauth2-redirect.html"
                },

                AllowedScopes =
                {
                    "bookstore"
                },

                AllowAccessTokensViaBrowser = true
            }
        };

    public static List<TestUser> Users =>
        new()
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "testuser",
                Password = "password"
            }
        };
}