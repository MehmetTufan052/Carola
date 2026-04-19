namespace Carola.DtoLayer.Dtos.EmailDtos
{
    public class BookingApprovalEmailDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CarDisplayName { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;
        public string TransmissionType { get; set; } = string.Empty;
        public string CarImageUrl { get; set; } = string.Empty;
        public decimal DailyPrice { get; set; }
        public int TotalDay { get; set; }
        public decimal TotalPrice { get; set; }
        public string CouponImageUrl { get; set; } = string.Empty;
        public string PromoImageUrl { get; set; } = string.Empty;
    }
}
