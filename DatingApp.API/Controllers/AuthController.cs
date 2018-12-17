using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using DatingApp.API.Models;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            // validate request in the future

            username = username.ToLower();
            if(await _repo.UserExists(username))
                return BadRequest("Username already exists");
            var userToCreate = new User
            {
                Username = username
            };
            ValuesController createdUser = await _repo.Register(userToCreate, password);

            return CreateAtRoute
        }
    }
}