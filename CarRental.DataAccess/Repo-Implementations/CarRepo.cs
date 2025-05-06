using CarRental.DataAccess.Data;
using CarRental.Entities.Models;
using CarRental.Entities.Repo_interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Repo_Implementations
{
    public class CarRepo : GenericRepo<Car>, ICarRepo
    {
        public CarRepo(ApplicationDbContext Context) : base(Context)
        {
           
        }

       
    }
}
