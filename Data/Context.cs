using Microsoft.EntityFrameworkCore;
using Mypcot.Models.Domain;

namespace Mypcot.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
}
