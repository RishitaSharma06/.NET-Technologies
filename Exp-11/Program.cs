using Microsoft.EntityFrameworkCore;
using SmartCampusPortal.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Register MVC + API Controllers
builder.Services.AddControllersWithViews();

// 2. Register SQL Server DbContext
builder.Services.AddDbContext<CampusDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampusDbConnection")));

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

// Enable Attribute Routing for Web API controllers (/api/...)
app.MapControllers();

// Default MVC Route opens directly to Smart Campus Portal
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ServiceRequests}/{action=Index}/{id?}");

app.Run();
