using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("cookie")
    .AddCookie("cookie")
    .AddOAuth("custom", o =>
    {
        o.SignInScheme = "cookie";
        o.ClientId = "x";
        o.ClientSecret = "x";

        o.AuthorizationEndpoint = "http://localhost:5005/oauth/authorize";
        o.TokenEndpoint = "http://localhost:5005/oauth/token";
        o.CallbackPath = "/oauth/custom-cb";

        o.UsePkce = true;

        o.ClaimActions.MapJsonKey("sub", "sub");
        o.Events.OnCreatingTicket = async ctx =>
        {
            // TODO: map claims
        };
    });

var app = builder.Build();

app.MapGet("/", (HttpContext ctx) =>
    {
        return ctx.User.Claims.Select(x =>
            new
            {
                x.Type, x.Value
            });
    }
);

app.MapGet("/login", () =>
{
    Results.Challenge(
        new AuthenticationProperties
        {
            RedirectUri = "http://localhost:3000/"
        },
        authenticationSchemes: new List<string> { "custom" }
    );
});

app.Run();