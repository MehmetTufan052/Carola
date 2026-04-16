using System;

namespace Carola.DtoLayer.Dtos.BookingDtos
{
    public class BookingClientFormDto
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int ReservationId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DocumentExpiryDate { get; set; }
        public string LicenseClass { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PickupLocationId { get; set; }
        public int ReturnLocationId { get; set; }
        public decimal TotalPrice { get; set; }

        public string Note { get; set; }
        public decimal DailyPrice { get; set; }
        public int TotalDay { get; set; }
        public string ReservationStatus { get; set; }
        public string Status { get; set; }
    }
}
