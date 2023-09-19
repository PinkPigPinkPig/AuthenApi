using AuthenApi.Data;
using AuthenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context) 
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if(_context.Users.Any(u => u.Username == request.Username)) 
            {
                return BadRequest("User already exists.");
            }

            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User successfully created!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if(user == null)
            {
                return NotFound();
            }

            if(user.Password != request.Password)
            {
                return BadRequest("Password is incorrect");
            }

            return Ok($"Welcome, {user.Username}!");
        }
    }
}
