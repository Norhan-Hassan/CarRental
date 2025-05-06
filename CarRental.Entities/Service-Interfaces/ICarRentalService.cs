using CarRental.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Service_Interfaces
{
    public interface ICarRentalService
    {
        Task UpdatePriceAsync(Car car);
        Task<int> DecreaseQuantityAsnc(Car car);
    }
}
