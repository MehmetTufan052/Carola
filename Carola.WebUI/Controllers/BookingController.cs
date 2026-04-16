using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.BookingDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IReservationService reservationService, ILocationService locationService, IMapper mapper)
        {
            _bookingService = bookingService;
            _reservationService = reservationService;
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BookingClient()
        {
            await LoadLocationsAsync();
            return View(CreateDefaultForm());
        }
       
        [HttpPost]
        public async Task<IActionResult> BookingClient(BookingClientFormDto model)
        {
            ValidateRequiredReferences(model);

            if (!ModelState.IsValid)
            {
                await LoadLocationsAsync();
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

        private static BookingClientFormDto CreateDefaultForm()
        {
            return new BookingClientFormDto
            {
                DailyPrice = DefaultDailyPrice,
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
