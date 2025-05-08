using CarRental.DataAccess.Data;
using CarRental.DataAccess.Repo_Implementations;
using CarRental.DataAccess.Service_Implementations;
using CarRental.Entities.Models;
using CarRental.Entities.Repo_interfaces;
using CarRental.Entities.Service_Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Extensions
{
    public static class DataBaseDependency
    {
        public static IServiceCollection DbDependencies(this IServiceCollection services,IConfiguration  configurations)
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(configurations.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICarRepo, CarRepo>();
            services.AddTransient<ICarRentalService, CarRentalService>();
            return services;
        }
        public static async Task SeedRolesMangerAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRoles.SeedRolesAsync(roleManager);
        }
    }
}
