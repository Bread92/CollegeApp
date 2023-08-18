using CollegeApp.Dtos;
using CollegeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services;

public interface IDirectorService
{
    public Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto createDto);
    public Task<ICollection<DirectorDto>> GetAllDirectorsAsync();
    public Task<DirectorDto?> GetOneAsync(Guid id);

    public Task<DirectorDto?> DeleteOneAsync(Guid id);

    public Task<DirectorDto?> UpdateAsync(DirectorDto updateDto, Guid id);
}

public class DirectorService : IDirectorService
{
    private readonly CollegeAppDbContext _dbContext;

    public DirectorService(CollegeAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto createDto)
    {
        var newDirector = new Director()
        {
            Id = new Guid(),
            Name = createDto.Name
        };
        
        _dbContext.Add(newDirector);
        await _dbContext.SaveChangesAsync();

        return newDirector.ToDto();
    }

    public async Task<ICollection<DirectorDto>> GetAllDirectorsAsync()
    {
        var entities = await _dbContext.Directors.ToListAsync();
        
        return entities.Select(x => x.ToDto()).ToList();
    }

    public async Task<DirectorDto?> GetOneAsync(Guid id)
    {
        var director = await _dbContext.Directors.FirstOrDefaultAsync(x => x.Id == id);

        return director?.ToDto();
    }

    public async Task<DirectorDto?> DeleteOneAsync(Guid id)
    {
        var director = await _dbContext.Directors.FirstOrDefaultAsync(x => x.Id == id);

        if (director is null)
        {
            return null;
        }
        
        _dbContext.Directors.Remove(director);

        return director.ToDto();
    }

    public async Task<DirectorDto?> UpdateAsync(DirectorDto updateDto, Guid id)
    {
        var director = await _dbContext.Directors.FirstOrDefaultAsync(x => x.Id == id);
        
        if (director is null)
        {
            return null;
        }

        director.Name = updateDto.Name;
        
        _dbContext.Update(director);
        
        await _dbContext.SaveChangesAsync();

        return director.ToDto();
    }
}