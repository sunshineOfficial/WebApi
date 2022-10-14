using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace DataAccess.EFCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Developer> Developers { get; set; }
    public DbSet<Project> Projects { get; set; }
}