
using CarRental.Entities.Models;
using CarRental.Entities.Repo_interfaces;
using CarRental.Entities.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Areas.Customer.Controllers
{
    [Area(Roles.CustomerRole)]
    [Authorize(Roles= Roles.CustomerRole)]
    public class RentController : Controller
    {
        private readonly ICarRepo _carRepo;
        private readonly ICarRentalService _carService;
        public RentController(ICarRepo carRepo,ICarRentalService carService)
        {
           _carRepo = carRepo;
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars =await _carRepo.GetTableNoTracking().ToListAsync();
            return View("Index",cars);
        }

        [HttpGet]
        public async Task<IActionResult> RentInAction(int id)
        {
            var car = await _carRepo.GetByIdAsync(id);

            int quantity= await _carService.DecreaseQuantityAsnc(car);

            if (quantity >= 0)
            {
                await _carRepo.SaveChangesAsync();

                TempData["Created"] = "Car is Rented successfully";
            }
            else
            {
                TempData["Deleted"] = "This Car is not available";
            }
           

            return RedirectToAction("Index");
        }

    }
}
