using CarRental.Entities.Models;
using CarRental.Entities.Repo_interfaces;
using CarRental.Entities.Service_Interfaces;
using CarRental.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Web.Areas.Admin.Controllers
{
    [Area(Roles.AdminRole)]
    [Authorize(Roles = Roles.AdminRole)]
    public class CarController : Controller
    {
        private readonly ICarRepo _carRepo;
        private readonly ICarRentalService _carService;
        public CarController(ICarRepo carRepo, ICarRentalService carService)
        {
            _carRepo = carRepo;
            _carService = carService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var availableCars = await _carRepo.GetTableNoTracking().ToListAsync();
           var carViewModelList = new List<CarViewModel>();

           if (availableCars.Any())
           {
                foreach (var car in availableCars)
                {
                    carViewModelList.Add(new CarViewModel
                    {
                        Id = car.ID,
                        Brand = car.Brand,
                        Price = car.Price,
                        Quantity = car.Quantity                      
                    });
                }
           }
           return View("Index", carViewModelList);
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel carViewModel)
        {
            if(ModelState.IsValid)
            {
                Car car = new Car();
                car.Brand = carViewModel.Brand;
                car.Price = carViewModel.Price;
                car.Quantity = carViewModel.Quantity;

                await _carRepo.AddAsync(car);
                int changes = await _carRepo.SaveChangesAsync();

                if (changes>0) {
                    return RedirectToAction("Index");
                }
            }
            return View("Create",carViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carRepo.GetByIdAsync(id);

            return View("Edit", car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditInAction(Car car)
        {
            if (ModelState.IsValid)
            {
                await _carService.UpdatePriceAsync(car);

                await _carRepo.SaveChangesAsync();

                int changes = await _carRepo.SaveChangesAsync();
                if (changes > 0)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return View("Edit", car);
           
        }


    }
}
