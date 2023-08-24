using CollegeApp.Dtos;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IRepairmenService
{
    public Task<RepairmanDto> CreateAsync(RepairmanCreateDto createDto);
    public Task<RepairmanDto?> GetOneAsync(Guid? repairmanId);
    public Task<RepairmanDto> UpdateAsync(Guid repairmanId, RepairmanDto updateDto);
    public Task<RepairmanDto> DeleteAsync(Guid? repairmanId);

    public Task<ICollection<RepairmanDto>> GetAllAsync();
}

public class RepairmenService : IRepairmenService
{
    private readonly CollegeAppDbContext _dbContext;

    public RepairmenService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RepairmanDto> CreateAsync(RepairmanCreateDto createDto)
    {
        var newRepairman = new Repairman()
        {
            RepairmanId = Guid.NewGuid(),
            FullName = createDto.FullName
        };

        _dbContext.Repairmen.Add(newRepairman);
        await _dbContext.SaveChangesAsync();

        return newRepairman.ToDto();
    }

    public async Task<RepairmanDto?> GetOneAsync(Guid? repairmanId)
    {
        var repairman = await _dbContext.Repairmen.FirstOrDefaultAsync(x => x.RepairmanId == repairmanId);

        return repairman?.ToDto();
    }

    public async Task<RepairmanDto> UpdateAsync(Guid repairmanId, RepairmanDto updateDto)
    {
        var repairmanToUpdate = await _dbContext.Repairmen.FirstAsync(x => x.RepairmanId == repairmanId);

        repairmanToUpdate.FullName = updateDto.FullName;

        await _dbContext.SaveChangesAsync();

        return repairmanToUpdate.ToDto();
    }

    public async Task<RepairmanDto> DeleteAsync(Guid? repairmanId)
    {
        var repairmanToDelete = await _dbContext.Repairmen.FirstAsync(x => x.RepairmanId == repairmanId);

        _dbContext.Remove(repairmanToDelete);

        await _dbContext.SaveChangesAsync();

        return repairmanToDelete.ToDto();
    }

    public async Task<ICollection<RepairmanDto>> GetAllAsync()
    {
        var allRepairmen = await _dbContext.Repairmen.Select(x => x.ToDto()).ToListAsync();

        return allRepairmen;
    }
}