using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Utilities
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

        public AppAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string savedToken = await localStorageService.GetItemAsync<string>("bearerToken");

                if (string.IsNullOrEmpty(savedToken))
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
                DateTime expiryDate = jwtSecurityToken.ValidTo;

                if (expiryDate < DateTime.UtcNow)
                {
                    await localStorageService.RemoveItemAsync("bearerToken");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                }

                var claims = ParseClaims(jwtSecurityToken);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

                return new AuthenticationState(user);



            }
            catch (Exception)
            {

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        private IList<Claim> ParseClaims(JwtSecurityToken jwtSecurityToken)
        {

            IList<Claim> claims = jwtSecurityToken.Claims.ToList();

            claims.Add(new Claim(ClaimTypes.Name, jwtSecurityToken.Subject));

            return claims;
        }

        internal async Task SignIn()
        {
            string savedToken = await localStorageService.GetItemAsync<string>("bearerToken");
            JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = ParseClaims(jwtSecurityToken);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            Task<AuthenticationState> authenticationState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authenticationState);

        }
        internal void SignOut()
        {
            ClaimsPrincipal nobody = new ClaimsPrincipal(new ClaimsIdentity());
            Task<AuthenticationState> authenticationState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authenticationState);
        }

    
    }
}
