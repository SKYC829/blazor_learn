using BlazorLearn_WebApp.Client.Components.L14;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

using System.Security.Claims;

namespace BlazorLearn_WebApp.Providers
{
    public class ServerSideAuthenticationStateProvider : AuthenticationStateProvider, IHostEnvironmentAuthenticationStateProvider, IDisposable
    {
        private readonly PersistentComponentState _persistentComponentState;
        private readonly PersistingComponentStateSubscription _persistingComponentStateSubscription;
        private Task<AuthenticationState>? _authenticationState;

        public ServerSideAuthenticationStateProvider(PersistentComponentState persistentComponentState)
        {
            _persistentComponentState = persistentComponentState;
            _persistingComponentStateSubscription = _persistentComponentState.RegisterOnPersisting(OnPersisting, RenderMode.InteractiveWebAssembly);
        }

        private async Task OnPersisting()
        {
            AuthenticationState state = await GetAuthenticationStateAsync();
            ClaimsPrincipal claimsPrincipal = state.User;
            if(claimsPrincipal is not null && (claimsPrincipal.Identity?.IsAuthenticated == true) )
            {
                _persistentComponentState.PersistAsJson(L14_Constant.MYJWT_AUTHENTICATION_CACHE_KEY, L14_UserInfo.FromClaimPrincipal(claimsPrincipal));
            }
        }

        public void Dispose()
        {
            _persistingComponentStateSubscription.Dispose();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authenticationState ?? throw new InvalidOperationException();

        public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask) => _authenticationState = authenticationStateTask;
    }
}
