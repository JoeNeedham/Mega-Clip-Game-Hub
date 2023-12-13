using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using User.Models;

namespace Auth.Services{
    
    public class AuthService
    {
        private readonly AppDbContext _context;

        private readonly UserProfile _userProfile;

        public AuthService(AppDbContext context, UserProfile userProfile)
        {
            _context = context;
            _userProfile = userProfile;
        }
        public string Authenticate(string? email, string? password)
        {
            string key = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            var tokenIssuer = "GameHub";
            var tokenAudience = "GameHubAPI";
            // Check for email
            var user = _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new Exception("Email is invalid");

            if (!_userProfile.VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Passowrd is incorrect");
            }
            // Create claims with user information
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Sub (subject) claim typically holds the user ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // Add other claims as needed
            };

            // Create a token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Set token expiration time
                Issuer = tokenIssuer,
                Audience = tokenAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        
        }
    }
}