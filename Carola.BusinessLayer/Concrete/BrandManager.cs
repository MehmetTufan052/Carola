using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        private readonly IMapper _mapper;
        private readonly IValidator<Brand> _validator;

        public BrandManager(IBrandDal brandDal, IValidator<Brand> validator, IMapper mapper)
        {
            _brandDal = brandDal;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var values = _mapper.Map<Brand>(createBrandDto);

            var result = await _validator.ValidateAsync(values);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _brandDal.InsertAsync(values);
        }

        public async Task DeleteBrandAsync(int id)
        {
            await _brandDal.DeleteAsync(id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            var values = await _brandDal.GetAllAsync();
            return _mapper.Map<List<ResultBrandDto>>(values);
        }

        public async Task<GetBrandByIdDto> GetBrandByIdAsync(int id)
        {
            var value = await _brandDal.GetByIdAsync(id);
            return _mapper.Map<GetBrandByIdDto>(value);
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            var values = _mapper.Map<Brand>(updateBrandDto);

            var result = await _validator.ValidateAsync(values);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _brandDal.UpdateAsync(values);
        }
    }
}
