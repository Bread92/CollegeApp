using CollegeApp.Dtos.MoldPurpose;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IMoldPurposesService
{
    public Task<MoldPurposeDto> CreateAsync(MoldPurposeCreateDto createDto);
    public Task<MoldPurposeDto?> GetOneAsync(Guid? moldPurposeId);
    public Task<MoldPurposeDto> UpdateAsync(Guid moldPurposeId, MoldPurposeUpdateDto updateDto);
    public Task<MoldPurposeDto> DeleteAsync(Guid? moldPurposeId);

    public Task<ICollection<MoldPurposeDto>> GetAllAsync();
}

public class MoldPurposesService : IMoldPurposesService
{
    private readonly CollegeAppDbContext _dbContext;

    public MoldPurposesService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MoldPurposeDto> CreateAsync(MoldPurposeCreateDto createDto)
    {
        var newMoldPurpose =  new MoldPurpose()
        {
            MoldPurposeId = Guid.NewGuid(),
            PurposeName = createDto.PurposeName
        };

        _dbContext.Add(newMoldPurpose);
        await _dbContext.SaveChangesAsync();

        return newMoldPurpose.ToDto();
    }

    public async Task<MoldPurposeDto?> GetOneAsync(Guid? moldPurposeId)
    {
        var moldPurpose = await _dbContext.MoldPurposes.FirstOrDefaultAsync(x => x.MoldPurposeId == moldPurposeId);

        return moldPurpose?.ToDto();
    }

    public async Task<MoldPurposeDto> UpdateAsync(Guid moldPurposeId, MoldPurposeUpdateDto updateDto)
    {
        var moldPurpose = await _dbContext.MoldPurposes.FirstAsync(x => x.MoldPurposeId == moldPurposeId);

        moldPurpose.PurposeName = updateDto.PurposeName;

        await _dbContext.SaveChangesAsync();

        var updatedDto = new MoldPurposeDto()
        {
            MoldPurposeId = moldPurpose.MoldPurposeId,
            PurposeName= moldPurpose.PurposeName
        };

        return updatedDto;
    }

    public async Task<MoldPurposeDto> DeleteAsync(Guid? moldPurposeId)
    {
        var moldPurposeToDelete = await _dbContext.MoldPurposes.FirstAsync(x => x.MoldPurposeId == moldPurposeId);

        _dbContext.MoldPurposes.Remove(moldPurposeToDelete);
        await _dbContext.SaveChangesAsync();
        
        return moldPurposeToDelete.ToDto();
    }

    public async Task<ICollection<MoldPurposeDto>> GetAllAsync()
    {
        var allMoldPurposes = await _dbContext.MoldPurposes.Select(x => x.ToDto()).ToListAsync();

        return allMoldPurposes;
    }
}

    