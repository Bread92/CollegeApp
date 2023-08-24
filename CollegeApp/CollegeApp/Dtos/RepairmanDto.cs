using CollegeApp.Entities;

namespace CollegeApp.Dtos;

public class RepairmanDto
{
    public Guid RepairmanId { get; set; }

    public string FullName { get; set; }
}

public static class RepairmanDtoExtensions
{
    public static RepairmanDto ToDto(this Repairman repairman)
    {
        return new RepairmanDto()
        {
            RepairmanId = repairman.RepairmanId,
            FullName= repairman.FullName
        };
    }
}