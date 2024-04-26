using BlazorLearn_ExampleApi.Databases;
using BlazorLearn_ExampleApi.Databases.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorLearn_ExampleApi.Controllers
{
    [Route("api/lobby")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly AuthExampleContext _exampleContext;

        public LobbyController(AuthExampleContext exampleContext)
        {
            _exampleContext = exampleContext;
        }

        [HttpPost("/login")]
        public async Task<string> LoginAsync([FromForm]string userName, [FromForm]string password, [FromForm]string? googleAccountId = null)
        {
            var users = _exampleContext.SystemUsers.Include(p=>p.SystemUserSecret).AsQueryable();
            if(users.Any(p=>(p.Name == userName || p.Email == userName) && (p.SystemUserSecret != null && p.SystemUserSecret.Password == password )) )
            {
                SystemUser su = users.First(p=>(p.Name == userName || p.Email == userName) && (p.SystemUserSecret != null && p.SystemUserSecret.Password == password ));
                if ( !string.IsNullOrWhiteSpace(googleAccountId) )
                {
                    if(!_exampleContext.SystemUserGoogleMaps.Any(p=>p.UserId == su.Id && p.GoogleOpenId == googleAccountId) )
                    {
                        SystemUserGoogleMap googleMap = new SystemUserGoogleMap();
                        googleMap.User = su;
                        googleMap.GoogleOpenId = googleAccountId;
                        _exampleContext.Add(googleMap);
                        await _exampleContext.SaveChangesAsync();
                    }
                }
                Claim[] basicClaims = [
                    new Claim(ClaimTypes.NameIdentifier,su.Id.ToString()),
                    new Claim(ClaimTypes.Name,su.Name),
                    new Claim(ClaimTypes.Email,su.Email),
                    new Claim(ClaimTypes.Hash,password)
                    ];
                Claim[] roleClaims = _exampleContext.SystemUserRoles.Include(p=>p.Role).Where(p=>p.UserId == su.Id).Select(p=>new Claim(ClaimTypes.Role,p.Role.Code)).ToArray();
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims:[..basicClaims,..roleClaims]);
                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            return "";
        }

        [HttpPost("/sigin-in")]
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
    }
}
