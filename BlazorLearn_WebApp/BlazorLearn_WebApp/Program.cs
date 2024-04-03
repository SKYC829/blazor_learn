using BlazorLearn_WebApp.Client;
using BlazorLearn_WebApp.Client.Components.L02.TransferServiceExample;
using BlazorLearn_WebApp.Client.Components.L09;
using BlazorLearn_WebApp.Client.Components.L12;
using BlazorLearn_WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents ()
    .AddInteractiveServerComponents ()
    .AddInteractiveWebAssemblyComponents ();

builder.Services.AddScoped<TransferLogService> ();

builder.Services.AddSingleton<SingletonService>()
    .AddScoped<ScopedService>()
    .AddTransient<TransientService>();

builder.Services.AddTransient<MyHttpClientMiddleware>()
    .AddTransient<YourHttpClientMiddleware>();

builder.Services.AddSingleton<WeatherForecastApi>();

builder.Services.AddHttpClient<HttpClient>("baidu",c =>
{
    c.BaseAddress = new Uri("https://www.baidu.com");
});

builder.Services.AddHttpClient("local", c =>
{
    c.BaseAddress = new Uri("https://localhost:7297");
}).AddHttpMessageHandler<MyHttpClientMiddleware>()
.AddHttpMessageHandler<YourHttpClientMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment () )
{
    app.UseWebAssemblyDebugging ();
}
else
{
    app.UseExceptionHandler ( "/Error" , createScopeForErrors: true );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ();
}

app.UseHttpsRedirection ();

app.UseStaticFiles ();
app.UseAntiforgery ();

app.MapRazorComponents<App> ()
    .AddInteractiveServerRenderMode ()
    .AddInteractiveWebAssemblyRenderMode ()
    .AddAdditionalAssemblies ( typeof ( BlazorLearn_WebApp.Client._Imports ).Assembly );

app.Run ();
