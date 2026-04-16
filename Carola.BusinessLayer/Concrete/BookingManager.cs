using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.BookingDtos;
using Carola.EntityLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class BookingManager : IBookingService
    {
        private readonly IBookingDal _bookingDal;
        private readonly IMapper _mapper;

        public BookingManager(IBookingDal bookingDal, IMapper mapper)
        {
            _bookingDal = bookingDal;
            _mapper = mapper;
        }

        public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
        {
            var value = _mapper.Map<Booking>(createBookingDto);
            value.CreatedDate = DateTime.Now;
            await _bookingDal.InsertAsync(value);
        }

        public async Task UpdateBookingAsync(UpdateBookingDto updateBookingDto)
        {
            var value = _mapper.Map<Booking>(updateBookingDto);
            await _bookingDal.UpdateAsync(value);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _bookingDal.DeleteAsync(id);
        }

        public async Task<List<ResultBookingDto>> GetAllBookingsAsync()
        {
            var values = await _bookingDal.GetAllAsync();
            return _mapper.Map<List<ResultBookingDto>>(values);
        }

        public async Task<GetBookingByIdDto> GetBookingByIdAsync(int id)
        {
            var value = await _bookingDal.GetByIdAsync(id);
            return _mapper.Map<GetBookingByIdDto>(value);
        }
    }
}
