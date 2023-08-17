using CollegeApp.Dtos;

namespace CollegeApp.Entities;

public class Director
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Workshop>? Workshops { get; set; }
    
}