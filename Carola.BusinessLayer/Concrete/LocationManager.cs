using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.LocationDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _locationDal;
        private readonly IMapper _mapper;

        public LocationManager(ILocationDal locationDal, IMapper mapper)
        {
            _locationDal = locationDal;
            _mapper = mapper;
        }

        public async Task CreateLocationAsync(CreateLocationDto createLocationDto)
        {
           var values=_mapper.Map<Location>(createLocationDto);
            await _locationDal.InsertAsync(values);
        }

        public async Task DeleteLocationAsync(int id)
        {
           await _locationDal.DeleteAsync(id);
        }

        public async Task<List<ResultLocationDto>> GetAllLocationAsync()
        {
            var values=await _locationDal.GetAllAsync();
            return _mapper.Map<List<ResultLocationDto>>(values);
        }

        public async Task<GetLocationByIdDto> GetLocationByIdAsync(int id)
        {
            var values=await _locationDal.GetByIdAsync(id);
            return _mapper.Map<GetLocationByIdDto>(values);
        }
       
        public Task UpdateLocationAsync(UpdateLocationDto updateLocationDto)
        {
           var values=_mapper.Map<Location>(updateLocationDto);
            return _locationDal.UpdateAsync(values);
        }
    }
}
