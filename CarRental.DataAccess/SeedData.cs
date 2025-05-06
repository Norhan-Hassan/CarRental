using CarRental.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess
{
    public static class SeedData
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.CustomerRole))
                await roleManager.CreateAsync(new IdentityRole(Roles.CustomerRole));

            if (!await roleManager.RoleExistsAsync(Roles.AdminRole))
                await roleManager.CreateAsync(new IdentityRole(Roles.AdminRole));
        }

    }
}
