namespace CollegeApp.Entities;

public class Workshop
{
    public Guid WorkshopId { get; set; }

    public string Name { get; set; }
    
    // Foreign Keys
    public Guid DirectorId { get; set; }
    public Guid SectorId { get; set; }
}