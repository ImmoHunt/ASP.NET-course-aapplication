using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.DTOs;
using System.Threading.Tasks;
using System.Security.Claims;

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

            userForRegisterDtos.Username = userForRegisterDtos.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDtos.Username))
                return BadRequest("Username already exists");
            var userToCreate = new User
            {
                Username = userForRegisterDtos.Username
            };
            var createdUser = await _repo.Register(userToCreate, userForRegisterDtos.Password);

            return StatusCode(201); // gotta fix this later when we get individual user
        }
        
        [HttpPost("login")]
        public async Task<IActionREsult> Login(UserForLoginDtos userForLoginDtos)
        {
            var userFromRepo = await _repo.Login(userForLoginDtos.Username, userForLoginDtos.Password);

            if(userFromRepo == null)
                return Unauthorized(); // to not give them hint that this user exists but the password is incorrect cause they can try brute force password
            
            //we will build up a token will contain user id and user's username
            var claims = new[]
            {
                new Claim()
            }

        }
    }
}