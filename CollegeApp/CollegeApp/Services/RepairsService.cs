using CollegeApp.Dtos.Repair;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IRepairsService
{
    public Task<RepairDto> CreateAsync(RepairCreateDto createDto);
    public Task<RepairDto?> GetOneAsync(Guid? repairId);
    public Task<RepairDto> UpdateAsync(Guid repairId, RepairUpdateDto updateDto);
    public Task<RepairDto> DeleteAsync(Guid? repairId);

    public Task<ICollection<RepairDto>> GetAllAsync();
}

public class RepairsService : IRepairsService
{
    private readonly CollegeAppDbContext _dbContext;

    public RepairsService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RepairDto> CreateAsync(RepairCreateDto createDto)
    {
        var newRepair =  new Repair()
        {
            RepairId = Guid.NewGuid(),
            RepairTime = createDto.RepairTime,
            MoldId = createDto.MoldId,
            RepairmanId = createDto.RepairmanId,
            Description = createDto.Description
        };

        _dbContext.Add(newRepair);
        await _dbContext.SaveChangesAsync();

        return newRepair.ToDto();
    }

    public async Task<RepairDto?> GetOneAsync(Guid? repairId)
    {
        var repair = await _dbContext.Repairs.FirstOrDefaultAsync(x => x.RepairId == repairId);

        return repair?.ToDto();
    }

    public async Task<RepairDto> UpdateAsync(Guid repairId, RepairUpdateDto updateDto)
    {
        var repair = await _dbContext.Repairs.FirstAsync(x => x.RepairId == repairId);

        repair.MoldId = updateDto.MoldId;
        repair.RepairmanId = updateDto.RepairmanId;
        repair.Description = updateDto.Description;

        await _dbContext.SaveChangesAsync();

        var updatedDto = new RepairDto()
        {
            RepairId = repair.RepairId,
            RepairTime = repair.RepairTime,
            MoldId = repair.MoldId,
            RepairmanId = repair.RepairmanId,
            Description = repair.Description
        };

        return updatedDto;
    }

    public async Task<RepairDto> DeleteAsync(Guid? repairId)
    {
        var repairToDelete = await _dbContext.Repairs.FirstAsync(x => x.RepairId == repairId);

        _dbContext.Repairs.Remove(repairToDelete);
        await _dbContext.SaveChangesAsync();
        
        return repairToDelete.ToDto();
    }

    public async Task<ICollection<RepairDto>> GetAllAsync()
    {
        var allDirectors = await _dbContext.Repairs.Select(x => x.ToDto()).ToListAsync();

        return allDirectors;
    }
}