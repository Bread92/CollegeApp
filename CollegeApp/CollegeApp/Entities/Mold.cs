namespace CollegeApp.Entities;

public class Mold //Press form
{
    public Guid MoldId { get; set; }

    public string Name { get; set; }
    public DateTime InstallationDate { get; set; }
    
    //Foreign Keys
    public Guid WorkshopId { get; set; }
}