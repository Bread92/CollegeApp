namespace CollegeApp.Entities;

public class Workshop
{
    public string Name { get; set; } // Цех
    
    public string Section { get; set; } // Участок
    
    public Guid DirectorId { get; set; } //Начальник
    
    public virtual Director Director { get; set; }
}