using CollegeApp.Dtos;
using CollegeApp.Entities;

namespace CollegeApp.Services;

public interface IDirectorService
{
    public Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto createDto);
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
}