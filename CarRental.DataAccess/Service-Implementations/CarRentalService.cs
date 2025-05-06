using CarRental.DataAccess.Data;
using CarRental.Entities.Models;
using CarRental.Entities.Repo_interfaces;
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
        private readonly ICarRepo _carRepo;
        public CarRentalService(ICarRepo carRepo)
        {
            _carRepo = carRepo;
        }
        public async Task<int> DecreaseQuantityAsnc(Car car)
        {
            //var carInDb = await _context.Cars.FirstOrDefaultAsync(c => c.ID == car.ID);
            var carInDb =await _carRepo.GetByIdAsync(car.ID);

            if (carInDb != null && carInDb.Quantity >= 0)
            {
                carInDb.Quantity = car.Quantity - 1;
            }
            return carInDb.Quantity;
        }

        public async Task UpdatePriceAsync(Car car)
        {
            //var carInDb = await _context.Cars.FirstOrDefaultAsync(c => c.ID == car.ID);
            var carInDb=await _carRepo.GetByIdAsync(car.ID);
            if (carInDb != null)
            {   
                carInDb.Price = car.Price;
            }
        }

        public async Task<int> RentCar(int id)
        {
            var car = await _carRepo.GetByIdAsync(id);
            int quantity = await DecreaseQuantityAsnc(car);
            return quantity;
        }
    }
}
