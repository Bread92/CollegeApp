namespace CollegeApp.Entities;

public class Repair
{
    public Guid RepairId { get; set; }

    public string Description { get; set; }

    public DateTime RepairTime { get; set; }
    
    //Foreign Keys
    public Guid RepairmanId { get; set; }
    public Guid MoldId { get; set; }
}