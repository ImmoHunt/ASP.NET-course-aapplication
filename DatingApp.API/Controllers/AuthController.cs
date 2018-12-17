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
        public async Task<IActionResult> Register(UserForRegisterDtos userForRegisterDtos)
        {
            // validate request in the future

            userForRegisterDtos.Username = username.ToLower();
            if(await _repo.UserExists(username))
                return BadRequest("Username already exists");
            var userToCreate = new User
            {
                Username = username
            };
            ValuesController createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201); // gotta fix this later when we get individual user
        }
        
    }
}