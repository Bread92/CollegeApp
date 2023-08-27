using CollegeApp.Dtos.Workshop;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IWorkshopsService
{
    public Task<WorkshopDto> CreateAsync(WorkshopCreateDto createDto);
    public Task<WorkshopDto?> GetOneAsync(Guid? workshopId);
    public Task<WorkshopDto> UpdateAsync(Guid workshopId, WorkshopUpdateDto updateDto);
    public Task<WorkshopDto> DeleteAsync(Guid? workshopId);

    public Task<ICollection<WorkshopDto>> GetAllAsync();
}

public class WorkshopsService : IWorkshopsService
{
    private readonly CollegeAppDbContext _dbContext;

    public WorkshopsService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkshopDto> CreateAsync(WorkshopCreateDto createDto)
    {
        var newWorkshop = new Workshop
        {
            WorkshopId = Guid.NewGuid(),
            Name = createDto.Name,
            DirectorId = createDto.DirectorId,
            SectorId = createDto.SectorId
        };

        _dbContext.Workshops.Add(newWorkshop);
        await _dbContext.SaveChangesAsync();

        return newWorkshop.ToDto();
    }

    public async Task<WorkshopDto?> GetOneAsync(Guid? workshopId)
    {
        var workshop = await _dbContext.Workshops.FirstOrDefaultAsync(x => x.WorkshopId == workshopId);

        return workshop?.ToDto();
    }

    public async Task<WorkshopDto> UpdateAsync(Guid workshopId, WorkshopUpdateDto updateDto)
    {
        var workshopToUpdate = await _dbContext.Workshops.FirstAsync(x => x.WorkshopId == workshopId);

        workshopToUpdate.Name = updateDto.Name;
        workshopToUpdate.DirectorId = updateDto.DirectorId;
        workshopToUpdate.SectorId = updateDto.SectorId;

        await _dbContext.SaveChangesAsync();

        return workshopToUpdate.ToDto();
    }

    public async Task<WorkshopDto> DeleteAsync(Guid? workshopId)
    {
        var workshopToDelete = await _dbContext.Workshops.FirstAsync(x => x.WorkshopId == workshopId);

        _dbContext.Remove(workshopToDelete);

        await _dbContext.SaveChangesAsync();

        return workshopToDelete.ToDto();
    }

    public async Task<ICollection<WorkshopDto>> GetAllAsync()
    {
        var allWorkshops = await _dbContext.Workshops.Select(x => x.ToDto()).ToListAsync();

        return allWorkshops;
    }
}