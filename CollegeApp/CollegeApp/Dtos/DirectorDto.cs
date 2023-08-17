using CollegeApp.Entities;

namespace CollegeApp.Dtos;

public class DirectorDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
}

public static class DirectorDtoExtensions
{
    public static DirectorDto ToDto(this Director director)
    {
        return new DirectorDto()
        {
            Id = director.Id,
            Name = director.Name
        };
    }
}
