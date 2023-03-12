global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
using TEST.Data;
//global using DynamicSelectCRUD.Data;
//global using DynamicSelectCRUD.Model;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer("server=DESKTOP-6UJ7BCL\\SQLEXPRESS;database=Akash;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"));

var app = builder.Build();

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
