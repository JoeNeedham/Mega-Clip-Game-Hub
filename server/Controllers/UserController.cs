using Microsoft.AspNetCore.Mvc;
using User.Models; // Import your user model namespace
using User.Services; // Import your user service namespace




public class UserDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    // Add other properties as needed
}



namespace User.Controllers
{
    [Route("api/user")] // attributes that declares the class as a route
    [ApiController] // attribute that declares the class as an api controller
    public class UserController : ControllerBase // a class named UserController that inherits ControllerBase - a controller class
    { 
        private readonly UserService _userService; // inject the UserService class and declare a class level variable (a field). Note it is not assigned yet just declared.

        public UserController(UserService userService) // give the UserController method the UserService class and assign it to the parameter userService
        {
            _userService = userService; // the class level variable is assigned in the constructor as an instance of UserService via the userService parameter
        }

        // POST: api/user
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserProfile userData)
        {
            if (userData == null)
            {
                return BadRequest("Please enter credentials");
            }

            try
            {
                if (!_userService.IsPasswordValid(userData.PasswordHash))
                {
                    return BadRequest("Password must include one uppercase character and one special character.");
                }

                userData.SetPassword(userData.PasswordHash);

                // Call the UserService to create the user
                _userService.CreateUser(userData.Username, userData.Email, userData.PasswordHash);

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during user creation
                return StatusCode(400, $"Bad request: {ex.Message}"); // redo the status codes 
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _userService.GetUser(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                // You may want to create a DTO (Data Transfer Object) to return only the necessary user information.
                // UserProfile might contain sensitive data that you don't want to expose.
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    // Add other properties as needed
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request: {ex.Message}");
            }
        }
        [HttpGet("get-username")]
        public IActionResult GetUsername(string username)
        {
            try
            {
                _userService.GetUsername(username);

                // if (username == null)
                // {
                //     return NotFound("User not found");
                // }

                return Ok("username available");
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad request: {ex.Message}");
            }
        }
        // create update request/method
        [HttpPost("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserProfile userData)
        {
            if (userData == null)
            {
                return BadRequest("Invalid user data");
            }

            try
            {
                if (!_userService.IsPasswordValid(userData.PasswordHash))
                {
                    return BadRequest("Password must include one uppercase character and one special character.");
                }
                
                // Call the SetPassword method to hash and set the new password
                userData.SetPassword(userData.PasswordHash);

                // Call the UserService to update the user
                _userService.UpdateUser(id, userData.Username, userData.Email, userData.PasswordHash);

                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during user update
                return StatusCode(400, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
