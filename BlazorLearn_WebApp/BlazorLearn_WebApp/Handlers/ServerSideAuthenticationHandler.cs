using BlazorLearn_WebApp.Client.Components.L13;
using BlazorLearn_WebApp.Client.Components.L14;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BlazorLearn_WebApp.Handlers
{
    public class ServerSideAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly L14_UserService _userService;
        private readonly MemoryStorageAccessor _storageAccessor;
        public ServerSideAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,L14_UserService userService,MemoryStorageAccessor memoryStorageAccessor) : base(options, logger, encoder)
        {
            _userService = userService;
            _storageAccessor = memoryStorageAccessor;
        }

        [Obsolete]
        public ServerSideAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,L14_UserService userService,MemoryStorageAccessor memoryStorageAccessor) : base(options, logger, encoder, clock)
        {
            _userService = userService;
            _storageAccessor = memoryStorageAccessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Logger.LogInformation($"获取认证结果:{Context.Connection.Id}");
            object? loginedToken = _storageAccessor.Get(L14_Constant.MYJWT_AUTHENTICATION_CACHE_KEY);
            if ( string.IsNullOrWhiteSpace(loginedToken?.ToString()) )
            {
                StringValues userName = StringValues.Empty,password = StringValues.Empty,provider = StringValues.Empty,openId = StringValues.Empty;
                if ( Request.Method == HttpMethods.Post )
                {
                    if ( !Request.HasFormContentType )
                    {
                        return AuthenticateResult.NoResult();
                    }
                    if ( Request.Form.TryGetValue("provider", out provider) )
                    {
                        switch ( provider.ToString().ToLower() )
                        {
                            case "google":
                                if(!Request.Form.TryGetValue("openId", out openId) )
                                {
                                    return AuthenticateResult.NoResult();
                                }
                                break;
                        }
                    }
                    else
                    {
                        if ( !Request.Form.TryGetValue("userName", out userName) || !Request.Form.TryGetValue("password", out password) )
                        {
                            return AuthenticateResult.NoResult();
                        }
                    }
                }
                else if ( Request.Method == HttpMethods.Get )
                {
                    if(!Request.Query.TryGetValue("provider",out provider) )
                    {
                        Request.Headers.TryGetValue("provider", out provider);
                    }
                    if ( string.IsNullOrWhiteSpace(provider) )
                    {
                        if ( !Request.Query.TryGetValue("userName", out userName) || !Request.Query.TryGetValue("password", out password) )
                        {
                            if ( !Request.Headers.TryGetValue("userName", out userName) || !Request.Headers.TryGetValue("password", out password) )
                            {
                                return AuthenticateResult.NoResult();
                            }
                        }
                    }
                    else
                    {
                        switch ( provider.ToString().ToLower() )
                        {
                            case "google":
                                if(!Request.Query.TryGetValue("openId",out openId) && !Request.Headers.TryGetValue("openId",out openId) )
                                {
                                    return AuthenticateResult.NoResult();
                                }
                                break;
                        }
                    }
                }
                else
                {
                    return AuthenticateResult.NoResult();
                }
                string? jwtToken = string.Empty;
                if ( string.IsNullOrWhiteSpace(provider) )
                {
                    if ( string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) )
                    {
                        return AuthenticateResult.Fail("请输入用户名或密码");
                    }
                    jwtToken = await _userService.LoginAsync(userName!,password!);
                }
                else
                {
                    if ( string.IsNullOrWhiteSpace(openId) )
                    {
                        return AuthenticateResult.Fail($"使用 {provider} 登录失败");
                    }
                    switch ( provider.ToString().ToLower() )
                    {
                        case "google":
                            JwtSecurityToken token = new JwtSecurityToken(openId);
                            openId = token.Claims.FirstOrDefault(p => p.Type == "sub")?.Value;
                            jwtToken = await _userService.LoginWithGoogleAsync(openId);
                            break;
                    }
                }
                if ( string.IsNullOrWhiteSpace(jwtToken) )
                {
                    return AuthenticateResult.Fail("用户名或密码错误");
                }
                _storageAccessor.Set(L14_Constant.MYJWT_AUTHENTICATION_CACHE_KEY, jwtToken);
                loginedToken = jwtToken;
            }
            JwtSecurityToken securityToken = new JwtSecurityToken(loginedToken!.ToString());
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, L14_Constant.MYJWT_AUTHENTICATION_TYPE)), L14_Constant.MYJWT_AUTHENTICATION_SCHEME_NAME));
        }
    }
}
