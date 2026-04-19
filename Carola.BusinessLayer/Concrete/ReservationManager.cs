using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.ReservationDtos;
using Carola.EntityLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class ReservationManager : IReservationService
    {
        private readonly IReservationDal _reservationDal;
        private readonly IMapper _mapper;

        public ReservationManager(IReservationDal reservationDal, IMapper mapper)
        {
            _reservationDal = reservationDal;
            _mapper = mapper;
        }

        public async Task<int> CreateReservationAsync(CreateReservationDto createReservationDto)
        {
            var value = _mapper.Map<Reservation>(createReservationDto);
            await _reservationDal.InsertAsync(value);
            return value.ReservationId;
        }

        public async Task UpdateReservationAsync(UpdateReservationDto updateReservationDto)
        {
            var currentReservation = await _reservationDal.GetByIdAsync(updateReservationDto.ReservationId);
            if (currentReservation == null)
            {
                return;
            }

            _mapper.Map(updateReservationDto, currentReservation);
            await _reservationDal.UpdateAsync(currentReservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationDal.DeleteAsync(id);
        }

        public async Task<List<ResultReservationDto>> GetAllReservationAsync()
        {
            var values = await _reservationDal.GetAllAsync();
            return _mapper.Map<List<ResultReservationDto>>(values);
        }

        public async Task<GetReservationByIdDto> GetReservationByIdAsync(int id)
        {
            var value = await _reservationDal.GetByIdAsync(id);
            return _mapper.Map<GetReservationByIdDto>(value);
        }
    }
}
