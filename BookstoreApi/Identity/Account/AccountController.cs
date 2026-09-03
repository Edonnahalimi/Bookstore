using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApi.Identity.Account;

public class AccountController : Controller
{
    [HttpGet("/Account/Login")]
    public IActionResult Login(string? returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost("/Account/Login")]
    public async Task<IActionResult> Login(
        string username,
        string password,
        string? returnUrl)
    {
        if (username != "testuser" ||
            password != "password")
        {
            ViewBag.ReturnUrl = returnUrl;

            ViewBag.Error =
                "Invalid username or password.";

            return View();
        }

        var claims = new List<Claim>
        {
            new Claim("sub", "1"),
            new Claim("name", "testuser")
        };

        var identity = new ClaimsIdentity(
            claims,
            IdentityServerConstants
                .DefaultCookieAuthenticationScheme,
            "name",
            "role");

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            IdentityServerConstants
                .DefaultCookieAuthenticationScheme,
            principal);

        if (string.IsNullOrEmpty(returnUrl))
        {
            return Redirect("/");
        }

        return Redirect(returnUrl);
    }
}