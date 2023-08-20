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
    public DbSet<Sector> Sectors { get; set; } = null!;
    public DbSet<Mold> Molds { get; set; } = null!;
    public DbSet<Repair> Repairs { get; set; } = null!;
    public DbSet<Repairman> Repairmen { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Director
        var directorModel = mb.Entity<Director>();
        directorModel.HasKey(x => x.DirectorId);

        //Workshop
        var workshopModel = mb.Entity<Workshop>();
        workshopModel.HasKey(x => x.WorkshopId);
        
        workshopModel.HasOne<Director>()
            .WithMany()
            .HasForeignKey(x => x.DirectorId);
        
        workshopModel.HasOne<Sector>()
            .WithMany()
            .HasForeignKey(x => x.SectorId);
        
        //Sector
        var sectorModel = mb.Entity<Sector>();
        sectorModel.HasKey(x => x.SectorId);
        
        //Mold
        var moldModel = mb.Entity<Mold>();
        moldModel.HasKey(x => x.MoldId);
        
        moldModel.HasOne<Workshop>()
            .WithMany()
            .HasForeignKey(x => x.WorkshopId);
        
        //Repair
        var repairModel = mb.Entity<Repair>();
        repairModel.HasKey(x => x.RepairId);
        
        repairModel.HasOne<Mold>()
            .WithMany()
            .HasForeignKey(x => x.MoldId);
        
        repairModel.HasOne<Repairman>()
            .WithMany()
            .HasForeignKey(x => x.RepairmanId);
        
        //Repairman
        var repairmanModel = mb.Entity<Repairman>();
        repairmanModel.HasKey(x => x.RepairmanId);
        
        base.OnModelCreating(mb);
    }

}