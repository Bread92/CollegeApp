namespace CollegeApp.Dtos.Sector;

public class SectorDto
{
    public Guid SectorId { get; set; }
    public string Name { get; set; }
}

public static class SectorDtoExtensions
{
    public static SectorDto ToDto(this Entities.Sector sector)
    {
        return new SectorDto()
        {
            SectorId = sector.SectorId,
            Name = sector.Name
        };
    }
}