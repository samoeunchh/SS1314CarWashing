using Microsoft.EntityFrameworkCore;
using SS1314CarWashing.Models;

namespace SS1314CarWashing;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Connection String
        var conString = @"Server=10.10.10.49;Database=SS13CarWashing;User Id=sa;Password=Strong.Pwd-123;TrustServerCertificate=true;";
        builder.Services.AddDbContext<AppDbContext>
            (o=>o.UseSqlServer(conString));
       
        // Add services to the container.
        builder.Services.AddControllersWithViews();

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
    }
}