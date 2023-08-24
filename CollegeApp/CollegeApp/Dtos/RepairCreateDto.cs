namespace CollegeApp.Dtos;

public class RepairCreateDto
{
    public DateTime RepairTime { get; set; }
    
    // Foreign keys

    public Guid MoldId { get; set; }

    public Guid RepairmanId { get; set; }
}