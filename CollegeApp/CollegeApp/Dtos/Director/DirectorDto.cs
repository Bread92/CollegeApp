namespace CollegeApp.Dtos.Director;

public class DirectorDto
{
    public Guid DirectorId { get; set; }

    public string FullName { get; set; }
}

public static class DirectorDtoExtensions
{
    public static DirectorDto ToDto(this Entities.Director director)
    {
        return new DirectorDto()
        {
            DirectorId = director.DirectorId,
            FullName = director.FullName
        };
    }
}