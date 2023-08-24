namespace CollegeApp.Dtos;

public class MoldCreateDto
{
    public string Name { get; set; }
    
    public DateTime InstallationDate { get; set; }

    // Foreign keys
    public Guid MoldPurposeId { get; set; }

    public Guid WorkshopId { get; set; }
}