using LibraryApi.Interfaces;
using LibraryApi.Models;
using LibraryApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService _userService)
        {
            this.userService = _userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Users user)
        {
            var newuser = await userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = newuser.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users updatedUser)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            await userService.DeleteUser(user.Id);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel logins)
        {
            var token = await userService.AuthenticateUser(logins);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
