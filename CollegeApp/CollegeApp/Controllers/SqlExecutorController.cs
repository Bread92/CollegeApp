using CollegeApp.Models;
using CollegeApp.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Execute(SqlQueryModel model)
    {
        return RedirectToAction("Index", new {query = model.Query});
    }
}