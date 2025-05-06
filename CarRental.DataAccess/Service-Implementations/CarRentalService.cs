using CarRental.DataAccess.Data;
using CarRental.Entities.Models;
using CarRental.Entities.Service_Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Service_Implementations
{
    public class CarRentalService:ICarRentalService
    {
        private readonly ApplicationDbContext _context;
        public CarRentalService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> DecreaseQuantityAsnc(Car car)
        {
            var carInDb = await _context.Cars.FirstOrDefaultAsync(c => c.ID == car.ID);
            if (carInDb != null && carInDb.Quantity >= 0)
            {
                carInDb.Quantity = car.Quantity - 1;
            }
            return carInDb.Quantity;
        }

        public async Task UpdatePriceAsync(Car car)
        {
            var carInDb = await _context.Cars.FirstOrDefaultAsync(c => c.ID == car.ID);

            if (carInDb != null)
            {
                carInDb.Price = car.Price;
            }
        }
    }
}
