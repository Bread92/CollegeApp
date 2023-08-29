using System.Data;
using Microsoft.Data.Sqlite;

namespace CollegeApp.Services;

public interface ISqlExecutorService
{
    public Task<DataTable> ExecuteQuery(string stringQuery);
}

public class SqlExecutorServiceService : ISqlExecutorService
{
    private readonly CollegeAppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public SqlExecutorServiceService(CollegeAppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    // Executer
    public async Task<DataTable> ExecuteQuery(string stringQuery)
    {
        await using var connection = new SqliteConnection("Data Source=college.db");
        
        connection.Open();
        var command = new SqliteCommand(stringQuery, connection);
        
        await using var reader = await command.ExecuteReaderAsync();

        var dataTable = new DataTable();
        dataTable.Load(reader);
        return dataTable;
    }
}