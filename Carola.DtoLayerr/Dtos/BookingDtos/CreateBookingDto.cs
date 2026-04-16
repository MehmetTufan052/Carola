using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DtoLayer.Dtos.BookingDtos
{
    public class CreateBookingDto
    {
        public int CarId { get; set; }
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

        public string Note { get; set; }

        public decimal DailyPrice { get; set; }
        public int TotalDay { get; set; }

        public string Status { get; set; }
    }

}
