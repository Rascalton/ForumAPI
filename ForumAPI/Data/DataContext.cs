using ForumAPI.Models.Entities;
using Microsoft.EntityFrameworkCore; // Add a reference to the Microsoft.EntityFrameworkCore assembly
namespace ForumAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<PostEntity> Posts { get; set; }
}