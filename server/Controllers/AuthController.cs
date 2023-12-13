using Microsoft.AspNetCore.Mvc;
using Auth.Services;

public class UserInfo
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    // Add other properties as needed
}


namespace Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController: ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService){
            _authService = authService;
        }

        [HttpPost("Login")]
        
        public IActionResult Login([FromBody] UserInfo userInfo)
        {
            if(userInfo == null)
            {
                return BadRequest("Please enter credentials");
            }

            string authToken = _authService.Authenticate(userInfo.Email, userInfo.Password);

            if (!string.IsNullOrEmpty(authToken))
            {
                return Ok(new { token = authToken }); // Return the authentication token
            }
            else
            {
                return Unauthorized("Authentication failed");
            }
        }
    }
}