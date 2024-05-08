using BlazorLearn_WebApp.Client.Components.L02.TransferServiceExample;
using BlazorLearn_WebApp.Client.Components.L09;
using BlazorLearn_WebApp.Client.Components.L12;
using BlazorLearn_WebApp.Client.Components.L13;
using BlazorLearn_WebApp.Client.Components.L14;
using BlazorLearn_WebApp.Client.Components.L14.Authorizes;
using BlazorLearn_WebApp.Client.Components.L14.GoogleLogin;
using BlazorLearn_WebApp.Components;
using BlazorLearn_WebApp.Handlers;
using BlazorLearn_WebApp.Providers;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;

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

builder.Services.AddScoped<CacheStorageAccessor>()
    .AddScoped<CookieStorageAccessor>()
    .AddScoped<IndexedDBAccessor>()
    .AddScoped<LocalStorageAccessor>()
    .AddSingleton<MemoryStorageAccessor>()
    .AddScoped<SessionStorageAccessor>();

builder.Services.AddScoped<L14_UserService>()
    .AddAuthentication(L14_Constant.MYJWT_AUTHENTICATION_SCHEME_NAME)
    .AddScheme<AuthenticationSchemeOptions,ServerSideAuthenticationHandler>(L14_Constant.MYJWT_AUTHENTICATION_SCHEME_NAME,null);
builder.Services.AddCascadingAuthenticationState()
    .AddScoped<AuthenticationStateProvider,ServerSideAuthenticationStateProvider>();

builder.Services.AddAuthorization(c =>
{
    c.DefaultPolicy = AuthorizationPolicy.Combine(c.DefaultPolicy, new AuthorizationPolicy([new RolesAuthorizationRequirement(["admin"])], []));
    c.AddPolicy("AdultPolicy", policy =>
    {
        policy.AddRequirements(new AdultAuthorizationRequirement(18));
    });
});

builder.Services.AddScoped<GoogleLoginApi>();
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
