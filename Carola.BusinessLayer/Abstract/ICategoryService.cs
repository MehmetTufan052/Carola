using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        Task DeleteCategoryAsync(int id);

        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task<GetCategoryByIdDto> GetCategoryByIdAsync(int id);

        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);

        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    }
}
