using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface ICarService 
    {
        Task DeleteCarAsync(int id);
        Task<List<ResultCarDto>> GetAllCarAsync();
        Task<GetCarByIdDto> GetCarByIdAsync(int id);
        Task CreateCarAsync(CreateCarDto createCarDto);
        Task UpdateCarAsync(UpdateCarDto updateCarDto);
        Task<List<ResultCarDto>> GetAllCarsWithCategoryAsync();
    }
}
