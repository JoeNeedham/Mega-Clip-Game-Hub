using Microsoft.AspNetCore.Mvc;
using User.Models; // Import your user model namespace
using User.Services; // Import your user service namespace

namespace User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/user
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserProfile userData)
        {
            if (userData == null)
            {
                return BadRequest("Invalid user data");
            }

            try
            {
                // Call the UserService to create the user
                _userService.CreateUser(userData.Username, userData.Email);

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during user creation
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
