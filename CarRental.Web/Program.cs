using CarRental.DataAccess.Data;
using CarRental.DataAccess.Extensions;
using CarRental.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Dependencies

            builder.Services.DbDependencies(builder.Configuration);

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();
                            
            #endregion

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

            app.UseAuthentication();

            app.UseAuthorization();

            #region SeedRoles
            await app.SeedRolesMangerAsync();
            #endregion

            app.MapControllerRoute(
               name: "Identity",
               pattern: "{area=Identity}/{controller=Account}/{action=Register}/{id?}");

            app.MapControllerRoute(
               name: "Admin",
               pattern: "{area=Admin}/{controller=Car}/{action=Index}/{id?}");

            app.MapControllerRoute(
               name: "Customer",
               pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
