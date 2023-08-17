using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public class CollegeAppDbContext : DbContext
{
    public CollegeAppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Director> Directors { get; set; } = null!;
    public DbSet<Workshop> Workshops { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder mb)
    {
        var directorModel = mb.Entity<Director>();
        directorModel.HasKey(x => x.Id);

        var workshopModel = mb.Entity<Workshop>();
        workshopModel.HasKey(x => x.Section);
        
        workshopModel.HasOne<Director>(x => x.Director)
            .WithMany(x => x.Workshops)
            .HasForeignKey(x => x.DirectorId);
        
        base.OnModelCreating(mb);
    }

}