// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using User.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Users = Set<UserProfile>(); // Initialize Users here
    }

    public DbSet<UserProfile> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your User entity here if needed
        modelBuilder.Entity<UserProfile>().ToTable("Users");
    }
}

// make sure to run - dotnet ef migrations add InitialCreate - and - dotnet ef database update - 
// this creates your database schema and then applies it
