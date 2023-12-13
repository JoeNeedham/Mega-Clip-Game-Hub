// Models/User.cs


namespace User.Models 
{
    public class UserProfile
    {
        public UserProfile()
        {
            // Initialize non-nullable properties here
            Username = "";
            Email = "";
            PasswordHash = "";
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public void SetPassword(string? password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
        public bool VerifyPassword(string? password, string? PasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
        // Add other properties as needed
    }
}

