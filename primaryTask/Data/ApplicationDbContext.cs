using Microsoft.EntityFrameworkCore;
using YourProjectName.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=your_server_address;Port=5432;Database=your_database_name;User Id=your_username;Password=your_password;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}