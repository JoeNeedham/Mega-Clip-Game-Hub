using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class User
    {
        public int Id { get; set; } // You can include an ID property for database usage

        [Required]
        [EmailAddress]
        [MaxLength(256)] 
        public string Email { get; set; }

        [Required]
        [MinLength(2)] 
        public required string Username { get; set; }

        [Required]
        [MinLength(6)] 
        public required string Password { get; set; }

        public User()
        {
            Email = "";
            Username = "";
            Password = "";
        }
    }
}
