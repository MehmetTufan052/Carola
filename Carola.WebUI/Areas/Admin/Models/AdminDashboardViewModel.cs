using Carola.DtoLayer.Dtos.BookingDtos;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.DtoLayer.Dtos.CustomerDtos;
using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.DtoLayer.Dtos.ReservationDtos;

namespace Carola.WebUI.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int TotalCategories { get; set; }
        public int TotalBrands { get; set; }
        public int ActiveBrands { get; set; }
        public int TotalBookings { get; set; }
        public int PendingBookings { get; set; }
        public int TotalReservations { get; set; }
        public int ActiveReservations { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalLocations { get; set; }
        public decimal FleetDailyValue { get; set; }
        public decimal BookingRevenue { get; set; }
        public decimal ReservationRevenue { get; set; }
        public double AverageCustomerAge { get; set; }

        public List<ResultCarDto> Cars { get; set; } = new();
        public List<ResultCategoryDto> Categories { get; set; } = new();
        public List<ResultBrandDto> Brands { get; set; } = new();
        public List<ResultBookingDto> Bookings { get; set; } = new();
        public List<ResultReservationDto> Reservations { get; set; } = new();
        public List<ResultCustomerDto> Customers { get; set; } = new();
        public List<ResultLocationDto> Locations { get; set; } = new();
    }
}
