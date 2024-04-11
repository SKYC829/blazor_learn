using BlazorLearn_WebApp.Client.Components.L13;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<CacheStorageAccessor>()
    .AddScoped<CookieStorageAccessor>()
    .AddScoped<LocalStorageAccessor>();
//builder.Services.AddSingleton<TransferLogService_1> ( c => TransferLogService_1.Instance );

await builder.Build ().RunAsync ();
