using BlazorLearn_WebApp.Client.Components.L13;
using BlazorLearn_WebApp.Client.Components.L14.GoogleLogin;
using BlazorLearn_WebApp.Client.Components.L14.Providers;

using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorLearn_WebApp.Client.Components.L14.Authorizes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<CacheStorageAccessor>()
    .AddScoped<CookieStorageAccessor>()
    .AddScoped<LocalStorageAccessor>()
    .AddSingleton<MemoryStorageAccessor>()
    .AddScoped<SessionStorageAccessor>();
//builder.Services.AddSingleton<TransferLogService_1> ( c => TransferLogService_1.Instance );

builder.Services.AddSingleton<AuthenticationStateProvider, ClientSideAuthenticationStateProvider>()
    .AddCascadingAuthenticationState();

builder.Configuration.AddUserSecrets<Program>(true,true);
builder.Services.AddScoped<GoogleLoginApi>();

builder.Services.AddHttpClient<HttpClient>("baidu", c =>
{
    c.BaseAddress = new Uri("https://www.baidu.com");
});

builder.Services.AddHttpClient("local", c =>
{
    c.BaseAddress = new Uri("https://localhost:7297");
});
builder.Services.AddAuthorizationCore(c =>
{
    c.DefaultPolicy = AuthorizationPolicy.Combine(c.DefaultPolicy, new AuthorizationPolicy([new RolesAuthorizationRequirement(["admin"])], []));
    c.AddPolicy("AdultPolicy", policy =>
    {
        policy.AddRequirements(new AdultAuthorizationRequirement(18));
    });
});
await builder.Build ().RunAsync ();
