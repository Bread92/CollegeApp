using System.Data;
using Microsoft.Data.Sqlite;
using Novacode;
using OfficeOpenXml;

namespace CollegeApp.Services;

public interface ISqlExecutorService
{
    public Task<DataTable> ExecuteQuery(string stringQuery);
    public Task<DocX> GetWordReport(string stringQuery);
    Task<ExcelPackage> GetExcelReport(string stringQuery);
}

public class SqlExecutorService : ISqlExecutorService
{
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
    public async Task<ExcelPackage> GetExcelReport(string stringQuery)
    {
        var dataTable = await ExecuteQuery(stringQuery);
        var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Report");

        for (var colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
        {
            worksheet.Cells[1, colIndex + 1].Value = dataTable.Columns[colIndex].ColumnName;
        }
        for (var rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
        {
            var dataRow = dataTable.Rows[rowIndex];
            for (var colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
            {
                worksheet.Cells[rowIndex + 2, colIndex + 1].Value = dataRow[colIndex];
            }
        }
        return package;
    }
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