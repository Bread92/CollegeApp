using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Novacode;

namespace CollegeApp.Services;

public interface ISqlExecutorService
{
    public Task<DataTable> ExecuteQuery(string stringQuery);
    public Task<DocX> GetWordReport(string stringQuery);
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

    public async Task<DocX> GetWordReport(string stringQuery)
    {
        var dataTable = await ExecuteQuery(stringQuery);
        var doc = DocX.Create("Report.docx");
        doc.InsertParagraph("SQL Query Report").FontSize(20d).Bold().Alignment = Alignment.center;

        var table = doc.AddTable(dataTable.Rows.Count + 1, dataTable.Columns.Count);
        table.Design = TableDesign.TableGrid;

        for (var colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
        {
            table.Rows[0].Cells[colIndex].Paragraphs.First().Append(dataTable.Columns[colIndex].ColumnName).Bold();
        }

        for (var rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
        {
            var dataRow = dataTable.Rows[rowIndex];
            for (var colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
            {
                table.Rows[rowIndex + 1].Cells[colIndex].Paragraphs.First().Append(dataRow[colIndex].ToString());
            }
        }
        doc.InsertTable(table);

        return doc;
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