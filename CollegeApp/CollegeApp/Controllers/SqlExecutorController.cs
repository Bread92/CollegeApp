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
        var doc = await _sqlExecutorService.GetWordReport(model.Query);
        
        var stream = new MemoryStream();
        doc.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);

        return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Report.docx", true);
    }
}