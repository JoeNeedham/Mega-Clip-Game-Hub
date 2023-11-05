using User.Models; // Import your user model namespace
using System.Text.RegularExpressions;


namespace User.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        public bool IsUsernameValid(string username)
        {
            return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,60}$");
        }

        public bool IsPasswordValid(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).+$"); // 1 uppercase, 1 lowercase, 1 special char, add char limit 8-128
        }
        public void CreateUser(string username, string email, string passwordhash)
        {   

            // Validate email
            if (!IsEmailValid(email))
            {
                throw new Exception("Invalid email format.");
            }

            // Validate username
            if (!IsUsernameValid(username))
            {
                throw new Exception("Invalid username format.");
            }
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordhash))
            {
                throw new ArgumentException("Username, email, and password cannot be empty or whitespace.");
            }

            // Check if the username is already in use, **create seperate route that checks for username**
            if (_context.Users.Any(u => u.Username == username))
            {
                throw new Exception("Username is already in use.");
            }

            // Check if the email is already in use
            if (_context.Users.Any(u => u.Email == email))
            {
                throw new Exception("Email is already in use.");
            }

            // You can create a User instance and save it to the database using Entity Framework Core
            var user = new UserProfile
            {
                Username = username,
                Email = email,
                PasswordHash = passwordhash,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public UserProfile? GetUser(int id)
        {  
        // Use Entity Framework Core to query the database for the user with the given id
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        return user;
        }

        public void GetUsername(string username)
        {  
        // Use Entity Framework Core to query the database for the user with the given id
            if (_context.Users.Any(u => u.Username == username))
            {
                throw new Exception("Username is already in use.");
            }
        }

        public void UpdateUser(int userId, string newUsername, string newEmail, string newPasswordHash) // make sure these are optional
        {
            // First, find the existing user in the database by their ID
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (existingUser != null)
            {
                // Validate username
                if (!IsUsernameValid(newUsername))
                {
                    throw new Exception("Invalid username format.");
                }

                if (string.IsNullOrWhiteSpace(newUsername) || string.IsNullOrWhiteSpace(newEmail) || string.IsNullOrWhiteSpace(newPasswordHash))
                {
                    throw new ArgumentException("Username, email, and password cannot be empty or whitespace.");
                }

                // Check if the username is already in use by a different user
                if (_context.Users.Any(u => u.Username == newUsername && u.Id != userId))
                {
                    throw new Exception("Username is already in use.");
                }

                // Check if the email is already in use by a different user
                if (_context.Users.Any(u => u.Email == newEmail && u.Id != userId))
                {
                    throw new Exception("Email is already in use.");
                }
                // Update the user's information
                existingUser.Username = newUsername;
                existingUser.Email = newEmail;
                existingUser.PasswordHash = newPasswordHash;

                // update other properties as needed.

                // Save the changes to the database
                _context.SaveChanges();
            }
        }
        public void DeleteUser(int userId)
        {
            // Use Entity Framework Core to find the user by their ID
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                _context.Users.Remove(user); // Mark the user for deletion
                _context.SaveChanges(); // Delete the user from the database
            }
            // You can handle cases where the user is not found, if necessary.
        }
    }
}
