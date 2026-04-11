using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.CarDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IMapper _mapper;

        public CarManager(ICarDal carDal, IMapper mapper)
        {
            _carDal = carDal;
            _mapper = mapper;
        }

        public async Task CreateCarAsync(CreateCarDto createCarDto)
        {
           var values= _mapper.Map<Car>(createCarDto);
            await _carDal.InsertAsync(values);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _carDal.DeleteAsync(id);
        }

        public async Task<List<ResultCarDto>> GetAllCarAsync()
        {
           var values=await _carDal.GetAllAsync();
           return _mapper.Map<List<ResultCarDto>>(values);
        }

        public async Task<GetCarByIdDto> GetCarByIdAsync(int id)
        {
           var values=await _carDal.GetByIdAsync(id);
           return _mapper.Map<GetCarByIdDto>(values);
        }

        public async Task<List<ResultCarDto>> TGetAllCarsWithCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCarAsync(UpdateCarDto updateCarDto)
        {
            var values= _mapper.Map<Car>(updateCarDto); 
            await _carDal.UpdateAsync(values);
        }
    }
}
