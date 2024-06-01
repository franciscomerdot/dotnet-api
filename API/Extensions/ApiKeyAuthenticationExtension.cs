using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using DotNetApi.API;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiKeyAuthenticationDefaults
{
    public const string AuthenticationScheme = "ApiKey";
}

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions { };

public class ApiKeyAuthenticationPostConfigureOptions : IPostConfigureOptions<ApiKeyAuthenticationOptions>
{
    public void PostConfigure(string? name, ApiKeyAuthenticationOptions options) { }
};

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private const string ApiKeyHeaderName = "X-API-Key";
    private const string ApiKeyQuery = "apiKey";
    private const string ApiKeySchemeName = ApiKeyAuthenticationDefaults.AuthenticationScheme;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<ApiKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKeyHeader = Request.Headers["X-API-Key"].FirstOrDefault();
        var apiKeyQuery = Request.Query["apiKey"].FirstOrDefault();

        var apiKey = apiKeyHeader ?? apiKeyQuery;

        var readerApiKey = Environment.GetEnvironmentVariable(Constants.API_KEY_READER_ENV);
        var adminApiKey = Environment.GetEnvironmentVariable(Constants.API_KEY_ADMIN_ENV);

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return AuthenticateResult.NoResult();
        }
        else if (!string.IsNullOrWhiteSpace(adminApiKey) && adminApiKey == apiKey)
        {
            return AuthenticateResult.Success(
                 new AuthenticationTicket(
                     new ClaimsPrincipal(new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, "Admin"),
                        new Claim(ClaimTypes.Role, Constants.READER_ROLE),
                        new Claim(ClaimTypes.Role, Constants.WRITER_ROLE)
                     }, ApiKeySchemeName)),
                     ApiKeySchemeName
                 )
             );
        }
        else if (!string.IsNullOrWhiteSpace(readerApiKey) && readerApiKey == apiKey)
        {
            return AuthenticateResult.Success(
                 new AuthenticationTicket(
                     new ClaimsPrincipal(new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, "Reader"),
                        new Claim(ClaimTypes.Role, Constants.READER_ROLE)
                     }, ApiKeySchemeName)),
                     ApiKeySchemeName
                 )
             );
        }
        else
        {
            return AuthenticateResult.Fail("Invalid API key");
        }
    }

    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 401;
        Response.Headers["WWW-Authenticate"] = "ApiKey";
        return Task.CompletedTask;
    }
}

public static class ApiKeyAuthenticationExtensions
{
    public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder)
    {
        return AddApiKey(builder, ApiKeyAuthenticationDefaults.AuthenticationScheme, _ => { });
    }

    public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder, string authenticationScheme, Action<ApiKeyAuthenticationOptions> configureOptions)
    {
        builder.Services.AddSingleton<IPostConfigureOptions<ApiKeyAuthenticationOptions>, ApiKeyAuthenticationPostConfigureOptions>();

        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
            authenticationScheme, configureOptions);
    }
}
