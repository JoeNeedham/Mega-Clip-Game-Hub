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
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        // Add other properties as needed
        // Add password later
    }
}

