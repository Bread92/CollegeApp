using CollegeApp.Dtos;
using CollegeApp.Dtos.Sector;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;
public interface ISectorsService
{
    public Task<SectorDto> CreateAsync(SectorCreateDto createDto);
    public Task<SectorDto?> GetOneAsync(Guid? sectorId);
    public Task<SectorDto> UpdateAsync(Guid sectorId, SectorUpdateDto updateDto);
    public Task<SectorDto> DeleteAsync(Guid? sectorId);

    public Task<ICollection<SectorDto>> GetAllAsync();
}

public class SectorsService : ISectorsService
{
    private readonly CollegeAppDbContext _dbContext;

    public SectorsService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SectorDto> CreateAsync(SectorCreateDto createDto)
    {
        var newSector = new Sector()
        {
            SectorId = Guid.NewGuid(),
            Name = createDto.Name
        };

        _dbContext.Sectors.Add(newSector);
        await _dbContext.SaveChangesAsync();

        return newSector.ToDto();
    }

    public async Task<SectorDto?> GetOneAsync(Guid? sectorId)
    {
        var sector = await _dbContext.Sectors.FirstOrDefaultAsync(x => x.SectorId == sectorId);

        return sector?.ToDto();
    }

    public async Task<SectorDto> UpdateAsync(Guid sectorId, SectorUpdateDto updateDto)
    {
        var sectorToUpdate = await _dbContext.Sectors.FirstAsync(x => x.SectorId == sectorId);

        sectorToUpdate.Name = updateDto.Name;

        await _dbContext.SaveChangesAsync();

        return sectorToUpdate.ToDto();
    }

    public async Task<SectorDto> DeleteAsync(Guid? sectorId)
    {
        var sectorToDelete = await _dbContext.Sectors.FirstAsync(x => x.SectorId == sectorId);

        _dbContext.Remove(sectorToDelete);

        await _dbContext.SaveChangesAsync();

        return sectorToDelete.ToDto();
    }

    public async Task<ICollection<SectorDto>> GetAllAsync()
    {
        var allSectors = await _dbContext.Sectors.Select(x => x.ToDto()).ToListAsync();

        return allSectors;
    }
}