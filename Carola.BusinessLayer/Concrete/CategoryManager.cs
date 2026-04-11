using AutoMapper;
using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var values= _mapper.Map<Category>(createCategoryDto);
            await _categoryDal.InsertAsync(values);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryDal.DeleteAsync(id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values= await _categoryDal.GetAllAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
           
        }

        public async Task<GetCategoryByIdDto> GetCategoryByIdAsync(int id)
        {
            var value =await _categoryDal.GetByIdAsync(id);
            return _mapper.Map<GetCategoryByIdDto>(value);
        }

        public async Task TDeleteAsync(int id)
        {
            await _categoryDal.DeleteAsync(id);
        }
        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var values= _mapper.Map<Category>(updateCategoryDto);
            await  _categoryDal.UpdateAsync(values);
        }
    }
}
