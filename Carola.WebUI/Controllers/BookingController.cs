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
        private const string DefaultStatus = "Onay Bekleniyor";

        private readonly IBookingService _bookingService;
        private readonly IReservationService _reservationService;
        private readonly ILocationService _locationService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, IReservationService reservationService, ILocationService locationService, ICarService carService, IMapper mapper, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _reservationService = reservationService;
            _locationService = locationService;
            _carService = carService;
            _mapper = mapper;
            _logger = logger;
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

            try
            {
                var createReservationDto = _mapper.Map<CreateReservationDto>(model);
                model.ReservationId = await _reservationService.CreateReservationAsync(createReservationDto);

                var createBookingDto = _mapper.Map<CreateBookingDto>(model);
                await _bookingService.CreateBookingAsync(createBookingDto);

                TempData["BookingClientSuccess"] = "Rezervasyon talebiniz basariyla alindi.";
                return RedirectToAction("BookingClient");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Booking kaydi olusturulurken hata olustu. CarId: {CarId}, ReservationId: {ReservationId}", model.CarId, model.ReservationId);
                ModelState.AddModelError(string.Empty, $"Kayit olusturulamadi: {ex.Message}");
                await LoadLocationsAsync();
                await LoadCarsAsync();
                return View(model);
            }
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
            if (model.CarId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Rezervasyon icin gecerli bir arac secimi gerekli.");
            }

            if (model.PickupLocationId <= 0 || model.ReturnLocationId <= 0)
            {
                ModelState.AddModelError(string.Empty, "Alis ve iade lokasyonlarini secmeniz gerekiyor.");
            }

            if (model.PickupDate == default || model.ReturnDate == default || model.ReturnDate < model.PickupDate)
            {
                ModelState.AddModelError(string.Empty, "Lutfen gecerli alis ve iade tarihleri girin.");
            }
        }
    }
}
