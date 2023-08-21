using CollegeApp.Dtos;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IDirectorsService
{
    public Task<DirectorDto> CreateAsync(DirectorCreateDto createDto);
    public Task<DirectorDto?> GetOneAsync(Guid? directorId);
    public Task<DirectorDto> UpdateAsync(Guid directorId, DirectorDto updateDto);
    public Task<DirectorDto> DeleteAsync(Guid? directorId);

    public Task<ICollection<DirectorDto>> GetAllAsync();
}

public class DirectorsService : IDirectorsService
{
    private readonly CollegeAppDbContext _dbContext;

    public DirectorsService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DirectorDto> CreateAsync(DirectorCreateDto createDto)
    {
        var newDirector =  new Director()
        {
            DirectorId = Guid.NewGuid(),
            FullName = createDto.FullName
        };

        _dbContext.Add(newDirector);
        await _dbContext.SaveChangesAsync();

        return newDirector.ToDto();
    }

    public async Task<DirectorDto?> GetOneAsync(Guid? directorId)
    {
        var director = await _dbContext.Directors.FirstOrDefaultAsync(x => x.DirectorId == directorId);

        return director?.ToDto();
    }

    //TODO: DirectorUpdateDto
    public async Task<DirectorDto> UpdateAsync(Guid directorId, DirectorDto updateDto)
    {
        var director = await _dbContext.Directors.FirstOrDefaultAsync(x => x.DirectorId == directorId);

        if (director is null)
        {
            return updateDto;
        }

        director.FullName = updateDto.FullName;

        await _dbContext.SaveChangesAsync();

        var updatedDto = new DirectorDto()
        {
            DirectorId = director.DirectorId,
            FullName = director.FullName
        };

        return updatedDto;
    }

    //TODO: get to know what to do in this case
    public async Task<DirectorDto> DeleteAsync(Guid? directorId)
    {
        var directorToDelete = await _dbContext.Directors.FirstOrDefaultAsync(x => x.DirectorId == directorId);

        if (directorToDelete is null)
        {
            //!
            throw new Exception("directorToDelete was null.");
        }
        
        _dbContext.Directors.Remove(directorToDelete);
        await _dbContext.SaveChangesAsync();
        return directorToDelete.ToDto();
    }

    public async Task<ICollection<DirectorDto>> GetAllAsync()
    {
        var allDirectors = await _dbContext.Directors.Select(x => x.ToDto()).ToListAsync();

        return allDirectors;
    }
}