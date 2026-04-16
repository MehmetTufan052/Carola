using Carola.DtoLayer.Dtos.BookingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingDto createBookingDto);
        Task UpdateBookingAsync(UpdateBookingDto updateBookingDto);

        Task DeleteBookingAsync(int id);

        Task<List<ResultBookingDto>> GetAllBookingsAsync();
        Task<GetBookingByIdDto> GetBookingByIdAsync(int id);

    }
}
