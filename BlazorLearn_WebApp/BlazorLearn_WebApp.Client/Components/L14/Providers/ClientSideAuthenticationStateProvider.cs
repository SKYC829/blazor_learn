using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Security.Claims;

namespace BlazorLearn_WebApp.Client.Components.L14.Providers
{
    public class ClientSideAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly PersistentComponentState _persistentComponentState;

        public ClientSideAuthenticationStateProvider(PersistentComponentState persistentComponentState)
        {
            _persistentComponentState = persistentComponentState;
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            if( _persistentComponentState.TryTakeFromJson(L14_Constant.MYJWT_AUTHENTICATION_CACHE_KEY,out L14_UserInfo? userInfo) && userInfo is not null)
            {
                claimsPrincipal = userInfo.ToClaimsPrincipal();
            }
            return Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
