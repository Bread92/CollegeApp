using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public class CollegeAppDbContext : DbContext
{
    public CollegeAppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Director> Directors { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);
    }

}