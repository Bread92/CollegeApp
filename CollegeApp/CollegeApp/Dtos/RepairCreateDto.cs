namespace CollegeApp.Dtos;

public class RepairCreateDto
{
    public DateTime RepairTime { get; set; }

    public string Description { get; set; }
    
    // Foreign keys

    public Guid MoldId { get; set; }

    public Guid RepairmanId { get; set; }
}