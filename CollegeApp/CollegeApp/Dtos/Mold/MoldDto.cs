namespace CollegeApp.Dtos.Mold;

public class MoldDto
{
    public Guid MoldId { get; set; }

    public string Name { get; set; }
    
    public DateTime InstallationDate { get; set; }

    // Foreign keys
    public Guid MoldPurposeId { get; set; }

    public Guid WorkshopId { get; set; }
}

public static class MoldDtoExtensions
{
    public static MoldDto ToDto(this Entities.Mold mold)
    {
        return new MoldDto()
        {
            MoldId = mold.MoldId,
            Name = mold.Name,
            WorkshopId = mold.WorkshopId,
            InstallationDate = mold.InstallationDate,
            MoldPurposeId = mold.MoldPurposeId
        };
    }
}