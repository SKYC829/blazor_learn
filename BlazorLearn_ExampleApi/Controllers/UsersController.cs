using BlazorLearn_ExampleApi.Databases;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorLearn_ExampleApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthExampleContext _exampleContext;

        public UsersController(AuthExampleContext exampleContext)
        {
            _exampleContext = exampleContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if(!_exampleContext.SystemUsers.Any(p=>p.Id == id) )
            {
                return NotFound();
            }
            var user = _exampleContext.SystemUsers.Include(p=>p.SystemUserRoles).ThenInclude(p=>p.Role).Include(p=>p.SystemUserGoogleMaps).First(p=>p.Id == id);
            return Ok(user);
        }
    }
}
