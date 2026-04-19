using Carola.BusinessLayer.Abstract;
using Carola.DtoLayer.Dtos.BookingDtos;
using Carola.DtoLayer.Dtos.EmailDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Carola.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IReservationService _reservationService;
        private readonly ICarService _carService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public BookingController(
            IBookingService bookingService,
            IReservationService reservationService,
            ICarService carService,
            IEmailService emailService,
            IMapper mapper)
        {
            _bookingService = bookingService;
            _reservationService = reservationService;
            _carService = carService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<IActionResult> BookingList()
        {
            ViewData["AdminSection"] = "Rezervasyonlar";
            ViewData["AdminPage"] = "Booking Listesi";
            ViewData["GlobalSearchPlaceholder"] = "Booking kayitlarinda ara";

            var values = await _bookingService.GetAllBookingsAsync();
            return View(values);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                TempData["BookingError"] = "Booking kaydi bulunamadi.";
                return RedirectToAction(nameof(BookingList));
            }

            var reservation = await _reservationService.GetReservationByIdAsync(booking.ReservationId);
            if (reservation == null)
            {
                TempData["BookingError"] = "Bagli reservation kaydi bulunamadi.";
                return RedirectToAction(nameof(BookingList));
            }

            var car = await _carService.GetCarByIdAsync(booking.CarId);
            if (car == null)
            {
                TempData["BookingError"] = "Teklif icin arac bilgisi bulunamadi.";
                return RedirectToAction(nameof(BookingList));
            }

            var updateBookingDto = _mapper.Map<UpdateBookingDto>(booking);
            updateBookingDto.Status = "Onaylandi";
            await _bookingService.UpdateBookingAsync(updateBookingDto);

            var updateReservationDto = _mapper.Map<UpdateReservationDto>(reservation);
            updateReservationDto.ReservationStatus = "Onaylandi";
            await _reservationService.UpdateReservationAsync(updateReservationDto);

            try
            {
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var emailModel = _mapper.Map<BookingApprovalEmailDto>(booking);
                _mapper.Map(car, emailModel);
                emailModel.CarImageUrl = BuildAbsoluteUrl(baseUrl, car.ImageUrl);
                emailModel.CouponImageUrl = $"{baseUrl}/images/email/discount-coupon.svg";
                emailModel.PromoImageUrl = $"{baseUrl}/images/email/promo-offer.svg";

                await _emailService.SendBookingApprovalOfferAsync(emailModel);

                TempData["BookingSuccess"] = "Booking onaylandi ve teklif e-postasi kullaniciya gonderildi.";
            }
            catch (Exception ex)
            {
                TempData["BookingError"] = $"Booking onaylandi ancak e-posta gonderilemedi: {ex.Message}";
            }

            return RedirectToAction(nameof(BookingList));
        }
        private static string BuildAbsoluteUrl(string baseUrl, string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return $"{baseUrl}/images/email/promo-offer.svg";
            }

            if (imageUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                imageUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return imageUrl;
            }

            return imageUrl.StartsWith("/")
                ? $"{baseUrl}{imageUrl}"
                : $"{baseUrl}/{imageUrl}";
        }
    }
}
