using CollegeApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<CollegeAppDbContext>(options =>
{
    options.UseSqlite("Data Source=college.db");
});

//Services
services.AddScoped<IDirectorsService, DirectorsService>();
services.AddScoped<IWorkshopsService, WorkshopsService>();
services.AddScoped<ISectorsService, SectorsService>();
services.AddScoped<IMoldPurposesService, MoldPurposesService>();
services.AddScoped<IRepairmenService, RepairmenService>();
services.AddScoped<IRepairsService, RepairsService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();