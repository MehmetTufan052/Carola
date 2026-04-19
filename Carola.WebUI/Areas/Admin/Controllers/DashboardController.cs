using Carola.BusinessLayer.Abstract;
using Carola.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IBookingService _bookingService;
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;

        public DashboardController(
            ICarService carService,
            ICategoryService categoryService,
            IBrandService brandService,
            IBookingService bookingService,
            IReservationService reservationService,
            ICustomerService customerService,
            ILocationService locationService)
        {
            _carService = carService;
            _categoryService = categoryService;
            _brandService = brandService;
            _bookingService = bookingService;
            _reservationService = reservationService;
            _customerService = customerService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Index()
        {
            // Services share the same scoped DbContext in this request,
            // so these EF queries must run sequentially, not in parallel.
            var cars = await _carService.GetAllCarsWithCategoryAsync() ?? new();
            var categories = await _categoryService.GetCategoriesWithCarCountAsync() ?? new();
            var brands = await _brandService.GetAllBrandAsync() ?? new();
            var bookings = await _bookingService.GetAllBookingsAsync() ?? new();
            var reservations = await _reservationService.GetAllReservationAsync() ?? new();
            var customers = await _customerService.GetAllCustomerAsync() ?? new();
            var locations = await _locationService.GetAllLocationAsync() ?? new();

            var today = DateTime.Today;

            var model = new AdminDashboardViewModel
            {
                Cars = cars,
                Categories = categories,
                Brands = brands,
                Bookings = bookings,
                Reservations = reservations,
                Customers = customers,
                Locations = locations,
                TotalCars = cars.Count,
                AvailableCars = cars.Count(x => x.IsAvailable || (!string.IsNullOrWhiteSpace(x.Status) && x.Status.Contains("musait", StringComparison.OrdinalIgnoreCase))),
                TotalCategories = categories.Count,
                TotalBrands = brands.Count,
                ActiveBrands = brands.Count(x => x.Status),
                TotalBookings = bookings.Count,
                PendingBookings = bookings.Count(x => !string.IsNullOrWhiteSpace(x.Status) &&
                    (x.Status.Contains("bekle", StringComparison.OrdinalIgnoreCase) ||
                     x.Status.Contains("onay bekleniyor", StringComparison.OrdinalIgnoreCase))),
                TotalReservations = reservations.Count,
                ActiveReservations = reservations.Count(x => x.ReturnDate.Date >= today && !string.IsNullOrWhiteSpace(x.ReservationStatus) && !x.ReservationStatus.Contains("ipt", StringComparison.OrdinalIgnoreCase)),
                TotalCustomers = customers.Count,
                TotalLocations = locations.Count,
                FleetDailyValue = cars.Sum(x => x.DailyPrice),
                BookingRevenue = bookings.Sum(x => x.DailyPrice * x.TotalDay),
                ReservationRevenue = reservations.Sum(x => x.TotalPrice),
                AverageCustomerAge = customers.Count == 0
                    ? 0
                    : customers.Average(x => (today - x.BirthDate.Date).TotalDays / 365.25)
            };

            ViewData["AdminSection"] = "Dashboard";
            ViewData["AdminPage"] = "Genel Bakis";
            ViewData["GlobalSearchPlaceholder"] = "Bu sayfada arama yok";

            return View(model);
        }
    }
}
