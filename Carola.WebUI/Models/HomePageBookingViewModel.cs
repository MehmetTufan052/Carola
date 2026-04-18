using Carola.DtoLayer.Dtos.CarDtos;
using Carola.DtoLayer.Dtos.LocationDtos;
using System;
using System.Collections.Generic;

namespace Carola.WebUI.Models
{
    public class HomePageBookingViewModel
    {
        public int? CarId { get; set; }
        public int? PickupLocationId { get; set; }
        public int? ReturnLocationId { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public List<ResultCarDto> Cars { get; set; } = new();
        public List<ResultLocationDto> Locations { get; set; } = new();
    }
}
