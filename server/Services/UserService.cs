using User.Models; // Import your user model namespace
using System;

namespace User.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateUser(string username, string email)
        {
            // You can create a User instance and save it to the database using Entity Framework Core
            var user = new UserProfile
            {
                Username = username,
                Email = email,
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
