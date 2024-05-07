using BlazorLearn_ExampleApi.Databases;
using BlazorLearn_ExampleApi.Databases.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorLearn_ExampleApi.Controllers
{
    [ApiController]
    [Route("api/lobby")]
    public class LobbyController : ControllerBase
    {
        private readonly AuthExampleContext _exampleContext;
        private readonly SystemUser _defaultUser;

        public LobbyController(AuthExampleContext exampleContext,SystemUser user)
        {
            _exampleContext = exampleContext;
            _defaultUser = user;
        }

        [HttpPost("login")]
        public async Task<string> LoginAsync([FromForm]string userName, [FromForm]string password, [FromForm]string? googleAccountId = null)
        {
            SystemUser? su = null;
            Claim[] roleClaims = [];
            try
            {
                var users = _exampleContext.SystemUsers.Include(p=>p.SystemUserSecret).AsQueryable();
                if ( users.Any(p => ( p.Name == userName || p.Email == userName ) && ( p.SystemUserSecret != null && p.SystemUserSecret.Password == password )) )
                {
                    su = users.First(p=>(p.Name == userName || p.Email == userName) && (p.SystemUserSecret != null && p.SystemUserSecret.Password == password ));
                    if ( !string.IsNullOrWhiteSpace(googleAccountId) )
                    {
                        if ( !_exampleContext.SystemUserGoogleMaps.Any(p => p.UserId == su.Id && p.GoogleOpenId == googleAccountId) )
                        {
                            SystemUserGoogleMap googleMap = new SystemUserGoogleMap();
                            googleMap.User = su;
                            googleMap.GoogleOpenId = googleAccountId;
                            _exampleContext.Add(googleMap);
                            await _exampleContext.SaveChangesAsync();
                        }
                    }
                    roleClaims = _exampleContext.SystemUserRoles.Include(p => p.Role).Where(p => p.UserId == su.Id).Select(p => new Claim(ClaimTypes.Role, p.Role.Code)).ToArray();
                }
            }
            catch
            {
                su = _defaultUser;
                roleClaims = su.SystemUserRoles.Select(p => new Claim(ClaimTypes.Role, p.Role.Code)).ToArray();
            }
            Claim[] basicClaims = [
                    new Claim(ClaimTypes.NameIdentifier,su?.Id.ToString()??"-1"),
                    new Claim(ClaimTypes.Name,su.Name),
                    new Claim(ClaimTypes.Email,su.Email),
                    new Claim(ClaimTypes.Hash,password)
                    ];
            Claim[] googleProperties = [];
            if ( !string.IsNullOrWhiteSpace(googleAccountId) )
            {
                googleProperties = [new Claim("GoogleOpenId", googleAccountId)];
            }
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims:[..basicClaims,..roleClaims,..googleProperties]);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        [HttpPost("sigin-in")]
        public async Task<IActionResult> SiginIn([FromForm]string userName, [FromForm]string email, [FromForm]string password, [FromForm]string? googleAccountId = null)
        {
            SystemUser su = new SystemUser();
            su.Name = userName;
            su.Email = email;
            su.SystemUserSecret = new SystemUserSecret()
            {
                User = su,
                Password = password
            };
            su.SystemUserRoles = new List<SystemUserRole>()
            {
                new SystemUserRole()
                {
                    User = su,
                    Role = _exampleContext.SystemRoles.First(p=>p.Code.ToLower() == "guest")
                }
            };
            if ( !string.IsNullOrWhiteSpace(googleAccountId) )
            {
                su.SystemUserGoogleMaps = new SystemUserGoogleMap()
                {
                    User = su,
                    GoogleOpenId = googleAccountId
                };
            }
            var entity = _exampleContext.Add(su);
            await _exampleContext.SaveChangesAsync();
            return Created("/users", entity.Entity.Id);
        }

        [HttpPost("login/google")]
        public async Task<string> LoginWithGoogleAsync([FromForm]string googleAccountId)
        {
            SystemUser? su = null;
            Claim[] roleClaims = [];
            try
            {
                su = _exampleContext.SystemUserGoogleMaps.Include(p=>p.User).ThenInclude(p=>p.SystemUserSecret).FirstOrDefault(p => p.GoogleOpenId == googleAccountId)?.User;
                if(su is null )
                {
                    return "";
                }
                roleClaims = _exampleContext.SystemUserRoles.Include(p => p.Role).Where(p => p.UserId == su.Id).Select(p => new Claim(ClaimTypes.Role, p.Role.Code)).ToArray();
            }
            catch ( Exception ex )
            {
                su = _defaultUser;
                su.SystemUserGoogleMaps = new SystemUserGoogleMap()
                {
                    Id = 1,
                    GoogleOpenId = googleAccountId,
                    UserId = su.Id
                };
                if(su.SystemUserSecret is null )
                {
                    su.SystemUserSecret = new SystemUserSecret()
                    {
                        Password = "123123",
                        Id = 1,
                        UserId = su.Id
                    };
                }
                roleClaims = su.SystemUserRoles.Select(p => new Claim(ClaimTypes.Role, p.Role.Code)).ToArray();
            }
            Claim[] basicClaims = [
                    new Claim(ClaimTypes.NameIdentifier,su?.Id.ToString()??"-1"),
                    new Claim(ClaimTypes.Name,su!.Name),
                    new Claim(ClaimTypes.Email,su.Email),
                    new Claim(ClaimTypes.Hash,su.SystemUserSecret!.Password),
                    new Claim("GoogleOpenId", googleAccountId)
                    ];
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims:[..basicClaims,..roleClaims]);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
