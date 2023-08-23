using CollegeApp.Entities;

namespace CollegeApp.Dtos;

public class SectorDto
{
    public Guid SectorId { get; set; }
    public string Name { get; set; }
}

public static class SectorDtoExtensions
{
    public static SectorDto ToDto(this Sector sector)
    {
        return new SectorDto()
        {
            SectorId = sector.SectorId,
            Name = sector.Name
        };
    }
}