using Carola.DtoLayer.Dtos.BrandDtos;
using Carola.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.BusinessLayer.Abstract
{
    public interface IBrandService
    {
        Task DeleteBrandAsync(int id);

        Task<List<Brand>> GetAllBrandAsync();  
        Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        Task CreateBrandAsync(CreateBrandDto createBrandDto);
        Task<Brand> GetBrandByIdAsync(int id);
    }
}
