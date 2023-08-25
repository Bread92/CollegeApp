using CollegeApp.Dtos.Mold;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IMoldsService
{
    public Task<MoldDto> CreateAsync(MoldCreateDto createDto);
    public Task<MoldDto?> GetOneAsync(Guid? moldId);
    public Task<MoldDto> UpdateAsync(Guid moldId, MoldUpdateDto updateDto);
    public Task<MoldDto> DeleteAsync(Guid? moldId);

    public Task<ICollection<MoldDto>> GetAllAsync();
}

public class MoldsService : IMoldsService
{
    private readonly CollegeAppDbContext _dbContext;

    public MoldsService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MoldDto> CreateAsync(MoldCreateDto createDto)
    {
        var newMold =  new Mold()
        {
            MoldId = Guid.NewGuid(),
            Name = createDto.Name,
            MoldPurposeId = createDto.MoldPurposeId,
            WorkshopId = createDto.WorkshopId,
            InstallationDate = createDto.InstallationDate
        };

        _dbContext.Add(newMold);
        await _dbContext.SaveChangesAsync();

        return newMold.ToDto();
    }

    public async Task<MoldDto?> GetOneAsync(Guid? moldId)
    {
        var director = await _dbContext.Molds.FirstOrDefaultAsync(x => x.MoldId == moldId);

        return director?.ToDto();
    }

    public async Task<MoldDto> UpdateAsync(Guid moldId, MoldUpdateDto updateDto)
    {
        var mold = await _dbContext.Molds.FirstAsync(x => x.MoldId == moldId);

        mold.Name = updateDto.Name;
        mold.MoldPurposeId = updateDto.MoldPurposeId;
        mold.WorkshopId = updateDto.WorkshopId;

        await _dbContext.SaveChangesAsync();

        var updatedDto = new MoldDto()
        {
            MoldId = mold.MoldId,
            InstallationDate = mold.InstallationDate,
            Name = mold.Name,
            WorkshopId = mold.WorkshopId,
            MoldPurposeId = mold.MoldPurposeId
        };

        return updatedDto;
    }

    public async Task<MoldDto> DeleteAsync(Guid? moldId)
    {
        var moldToDelete = await _dbContext.Molds.FirstAsync(x => x.MoldId == moldId);

        _dbContext.Molds.Remove(moldToDelete);
        await _dbContext.SaveChangesAsync();
        
        return moldToDelete.ToDto();
    }

    public async Task<ICollection<MoldDto>> GetAllAsync()
    {
        var allMolds = await _dbContext.Molds.Select(x => x.ToDto()).ToListAsync();

        return allMolds;
    }
}