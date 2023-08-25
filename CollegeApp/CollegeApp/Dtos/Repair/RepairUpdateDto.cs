namespace CollegeApp.Dtos.Repair;

public class RepairUpdateDto
{
    public string Description { get; set; }
    
    // Foreign keys

    public Guid MoldId { get; set; }

    public Guid RepairmanId { get; set; }
}