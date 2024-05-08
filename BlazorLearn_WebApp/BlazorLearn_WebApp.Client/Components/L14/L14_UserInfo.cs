using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorLearn_WebApp.Client.Components.L14
{
    public class L14_UserInfo
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Age { get; set; } = 10;

        public virtual string? SystemUserGoogleMaps { get; set; }

        public virtual ICollection<string> SystemUserRoles { get; set; } = new List<string>();

        public virtual string? SystemUserSecret { get; set; }

        public static L14_UserInfo? FromJwtString(string jwtString)
        {
            try
            {
                JwtSecurityToken token = new JwtSecurityToken(jwtString);
                return FromClaimPrincipal(new ClaimsPrincipal(new ClaimsIdentity(token.Claims)));
            }
            catch ( Exception )
            {
                return null;
            }
        }

        public static L14_UserInfo? FromClaimPrincipal(ClaimsPrincipal? claimsPrincipal)
        {
            if(claimsPrincipal is null )
            {
                return null;
            }
            try
            {
                L14_UserInfo info = new L14_UserInfo();
                info.Id = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");
                info.Name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? "";
                info.Email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value ?? "";
                info.Age = int.Parse(claimsPrincipal.FindFirst("age")?.Value ?? "0");
                info.SystemUserGoogleMaps = claimsPrincipal.FindFirst("GoogleOpenId")?.Value ?? "";
                info.SystemUserSecret = claimsPrincipal.FindFirst(ClaimTypes.Hash)?.Value ?? "";
                info.SystemUserRoles = new List<string>(claimsPrincipal.FindAll(ClaimTypes.Role).Select(p => p.Value));
                return info;
            }
            catch ( Exception )
            {
                return null;
            }
        }

        public string ToJwtString()
        {
            ClaimsPrincipal claimsPrincipal = ToClaimsPrincipal();
            JwtSecurityToken token = new JwtSecurityToken(claims:claimsPrincipal.Claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ToClaimsPrincipal()
        {
            Claim[] basicClaims = [
                    new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                    new Claim(ClaimTypes.Name,Name),
                    new Claim(ClaimTypes.Email,Email),
                    new Claim(ClaimTypes.Hash,SystemUserSecret),
                    new Claim("age",Age.ToString())
                    ];
            Claim[] roleClaims = SystemUserRoles.Select(p=>new Claim(ClaimTypes.Role,p)).ToArray();
            Claim[] googleProperties = [];
            if ( !string.IsNullOrWhiteSpace(SystemUserGoogleMaps) )
            {
                googleProperties = [new Claim("GoogleOpenId", SystemUserGoogleMaps)];
            }
            return new ClaimsPrincipal(new ClaimsIdentity(claims: [.. basicClaims, .. roleClaims, .. googleProperties], authenticationType: L14_Constant.MYJWT_AUTHENTICATION_TYPE));
        }
    }
}
