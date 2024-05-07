using BlazorLearn_WebApp.Client.Components.L13;
using BlazorLearn_WebApp.Client.Components.L14.Providers;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<CacheStorageAccessor>()
    .AddScoped<CookieStorageAccessor>()
    .AddScoped<LocalStorageAccessor>()
    .AddSingleton<MemoryStorageAccessor>()
    .AddScoped<SessionStorageAccessor>();
//builder.Services.AddSingleton<TransferLogService_1> ( c => TransferLogService_1.Instance );

builder.Services.AddSingleton<AuthenticationStateProvider, ClientSideAuthenticationStateProvider>()
    .AddCascadingAuthenticationState();

builder.Services.AddHttpClient<HttpClient>("baidu", c =>
{
    c.BaseAddress = new Uri("https://www.baidu.com");
});

builder.Services.AddHttpClient("local", c =>
{
    c.BaseAddress = new Uri("https://localhost:7297");
});

await builder.Build ().RunAsync ();
