using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using System.Security.Principal;

[Route("api/[controller]")]
[ApiController]
public class GauthController : ControllerBase
{
    public class GoogleSignInRequest
    {
        public string? IdToken { get; set; }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Provider { get; set; }
    }

    [HttpGet]
    public IActionResult Get()
    {
        var response = new
        {
            message = "gauth"
        };

        return Ok(response);
    }

    [HttpPost]
    public IActionResult Post([FromBody] GoogleSignInRequest payload)
    {

        string payloadJson = System.Text.Json.JsonSerializer.Serialize(payload);
        Console.WriteLine(payloadJson);

        // var payload = GoogleJsonWebSignature.ValidateAsync(payload.idToken, new GoogleJsonWebSignature.ValidationSettings()).Result;

        var responseData = new
        {
            Message = "Request processed successfully",
            ReceivedData = payload 
        };

        var responseJson = System.Text.Json.JsonSerializer.Serialize(responseData);

        return Ok(new
        {
            token = payload.IdToken,
            name = payload.Name,
            email = payload.Email,
        });

    }
}
