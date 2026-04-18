using Carola.BusinessLayer.Abstract;
using Carola.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Carola.WebUI.ViewComponents.HomePage
{
    public class _HomePageBookingComponentPartial : ViewComponent
    {
        private readonly ILocationService _locationService;
        private readonly ICarService _carService;

        public _HomePageBookingComponentPartial(ILocationService locationService, ICarService carService)
        {
            _locationService = locationService;
            _carService = carService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var locations = await _locationService.GetAllLocationAsync();
            var cars = await _carService.GetAllCarsWithCategoryAsync();

            var availableCars = cars
                .Where(x => x.IsAvailable)
                .OrderBy(x => x.DailyPrice)
                .ToList();

            var model = new HomePageBookingViewModel
            {
                Cars = availableCars,
                Locations = locations,
                CarId = availableCars.FirstOrDefault()?.CarId,
                PickupLocationId = locations.FirstOrDefault()?.LocationId,
                ReturnLocationId = locations.Skip(1).FirstOrDefault()?.LocationId ?? locations.FirstOrDefault()?.LocationId,
                PickupDate = DateTime.Today.AddDays(1),
                ReturnDate = DateTime.Today.AddDays(4)
            };

            return View("~/Views/Shared/Components/HomePage/_HomePageBookingComponentPartial/Default.cshtml", model);
        }
    }
}
