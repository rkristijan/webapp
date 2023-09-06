using Microsoft.EntityFrameworkCore;
using webapp.Data;
using webapp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var dbHost = Environment.GetEnvironmentVariable("dbHost");
//var dbName = Environment.GetEnvironmentVariable("dbName");
//var dbSaPassword = Environment.GetEnvironmentVariable("dbSaPassword");

//var connectionString = $"Data Source={dbHost}; Initial Catalog= {dbName}; User ID=sa; Password={dbSaPassword}; TrustServerCertificate=True";

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("WebappConnection"));
var app = builder.Build();


DbPrep.Preparation(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
