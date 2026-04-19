using Carola.DtoLayer.Dtos.EmailDtos;

namespace Carola.BusinessLayer.Abstract
{
    public interface IEmailService
    {
        Task SendBookingApprovalOfferAsync(BookingApprovalEmailDto model);
    }
}
