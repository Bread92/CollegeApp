namespace CollegeApp.Dtos.Mold;

public class MoldUpdateDto
{
    public string Name { get; set; }
    
    // Foreign keys
    public Guid MoldPurposeId { get; set; }

    public Guid WorkshopId { get; set; }
}