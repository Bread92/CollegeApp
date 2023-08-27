using System.Data;

namespace CollegeApp.Models;

public class SqlQueryModel
{
    public string Query { get; set; } = "";
    public DataTable? Result { get; set; }
    
    public string? ErrorMessage { get; set; }
}