using CollegeApp.Models;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Novacode;

namespace CollegeApp.Controllers;

public class SqlExecutorController : Controller
{
    private readonly ISqlExecutorService _sqlExecutorService;

    public SqlExecutorController(ISqlExecutorService sqlExecutorService)
    {
        _sqlExecutorService = sqlExecutorService;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] SqlQueryModel? model)
    {
        if (model is null)
            return View(new SqlQueryModel());

        try
        {
            model.Result = await _sqlExecutorService.ExecuteQuery(model.Query);
            model.ErrorMessage = null;
            return View("Index", model);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            return View("Index", model);
        }
    }

    [HttpPost]
    public IActionResult Execute(SqlQueryModel model)
    {
        return RedirectToAction("Index", new {query = model.Query});
    }
    
    [HttpPost]
    public async Task<IActionResult> GenerateReport(SqlQueryModel model)
    {
        model.Result = await _sqlExecutorService.ExecuteQuery(model.Query);
        try
        {
            var doc = DocX.Create("Report.docx");
            doc.InsertParagraph("SQL Query Report").FontSize(20d).Bold().Alignment = Alignment.center;

            if (model.Result != null)
            {
                var table = doc.AddTable(model.Result.Rows.Count + 1, model.Result.Columns.Count);
                table.Design = TableDesign.TableGrid;

                for (var colIndex = 0; colIndex < model.Result.Columns.Count; colIndex++)
                {
                    table.Rows[0].Cells[colIndex].Paragraphs.First().Append(model.Result.Columns[colIndex].ColumnName).Bold();
                }
                for (var rowIndex = 0; rowIndex < model.Result.Rows.Count; rowIndex++)
                {
                    var dataRow = model.Result.Rows[rowIndex];
                    for (var colIndex = 0; colIndex < model.Result.Columns.Count; colIndex++)
                    {
                        table.Rows[rowIndex + 1].Cells[colIndex].Paragraphs.First().Append(dataRow[colIndex].ToString());
                    }
                }
                doc.InsertTable(table);
            }
            var stream = new MemoryStream();
            doc.SaveAs(stream);
            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Report.docx", true);
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", new { errorMessage = ex.Message });
        }
    }
}