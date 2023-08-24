using CollegeApp.Entities;

namespace CollegeApp.Dtos;

public class RepairDto
{
    public Guid RepairId { get; set; }

    public DateTime RepairTime { get; set; }
    
    // Foreign keys

    public Guid MoldId { get; set; }

    public Guid RepairmanId { get; set; }
}

public static class RepairDtoExtensions
{
    public static RepairDto ToDto(this Repair repair)
    {
        return new RepairDto()
        {
            RepairId = repair.RepairId,
            RepairTime = repair.RepairTime,
            MoldId = repair.MoldId,
            RepairmanId = repair.RepairmanId
        };
    }
}