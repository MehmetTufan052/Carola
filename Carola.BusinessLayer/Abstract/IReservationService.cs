using Carola.DtoLayer.Dtos.ReservationDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface IReservationService
    {
        Task DeleteReservationAsync(int id);
        Task<int> CreateReservationAsync(CreateReservationDto createReservationDto);
        Task UpdateReservationAsync(UpdateReservationDto updateReservationDto);
        Task<List<ResultReservationDto>> GetAllReservationAsync();
        Task<GetReservationByIdDto> GetReservationByIdAsync(int id);
    }
}
