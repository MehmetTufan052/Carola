using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.BookingDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Carola.WebUI.Controllers
{
    public class BookingController : Controller
    {
        private const decimal DefaultDailyPrice = 1100;
        private const string DefaultStatus = "Beklemede";

        private readonly IBookingService _bookingService;
        private readonly IReservationService _reservationService;
        private readonly ILocationService _locationService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IReservationService reservationService, ILocationService locationService, ICarService carService, IMapper mapper)
        {
            _bookingService = bookingService;
            _reservationService = reservationService;
            _locationService = locationService;
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BookingClient(int? carId, int? pickupLocationId, int? returnLocationId, DateTime? pickupDate, DateTime? returnDate)
        {
            await LoadLocationsAsync();
            await LoadCarsAsync();
            return View(await CreateDefaultFormAsync(carId, pickupLocationId, returnLocationId, pickupDate, returnDate));
        }
       
        [HttpPost]
        public async Task<IActionResult> BookingClient(BookingClientFormDto model)
        {
            ValidateRequiredReferences(model);

            if (!ModelState.IsValid)
            {
                await LoadLocationsAsync();
                await LoadCarsAsync();
                return View(model);
            }

            var createReservationDto = _mapper.Map<CreateReservationDto>(model);
            model.ReservationId = await _reservationService.CreateReservationAsync(createReservationDto);

            var createBookingDto = _mapper.Map<CreateBookingDto>(model);
            await _bookingService.CreateBookingAsync(createBookingDto);
            return RedirectToAction("BookingClient");
        }

        private async Task LoadLocationsAsync()
        {
            ViewBag.Locations = await _locationService.GetAllLocationAsync();
        }

        private async Task LoadCarsAsync()
        {
            ViewBag.Cars = await _carService.GetAllCarsWithCategoryAsync();
        }

        private async Task<BookingClientFormDto> CreateDefaultFormAsync(int? carId, int? pickupLocationId, int? returnLocationId, DateTime? pickupDate, DateTime? returnDate)
        {
            var cars = await _carService.GetAllCarsWithCategoryAsync();
            var selectedCar = cars.FirstOrDefault(x => x.CarId == carId) ?? cars.FirstOrDefault(x => x.IsAvailable);
            var effectivePickupDate = pickupDate?.Date ?? DateTime.Today.AddDays(1);
            var effectiveReturnDate = returnDate?.Date ?? effectivePickupDate.AddDays(3);
            var totalDay = Math.Max(1, (effectiveReturnDate - effectivePickupDate).Days);
            var dailyPrice = selectedCar?.DailyPrice ?? DefaultDailyPrice;

            return new BookingClientFormDto
            {
                CarId = selectedCar?.CarId ?? 0,
                PickupLocationId = pickupLocationId ?? 0,
                ReturnLocationId = returnLocationId ?? pickupLocationId ?? 0,
                PickupDate = effectivePickupDate,
                ReturnDate = effectiveReturnDate,
                DailyPrice = dailyPrice,
                TotalDay = totalDay,
                TotalPrice = totalDay * dailyPrice,
                ReservationStatus = DefaultStatus,
                Status = DefaultStatus
            };
        }

        private void ValidateRequiredReferences(BookingClientFormDto model)
        {
            if (model.CarId <= 0 || model.CustomerId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Rezervasyon icin CarId ve CustomerId bilgisi gerekli.");
            }
        }
    }
}
