using Carola.DtoLayer.Dtos.BrandDtos;
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

        Task<List<ResultBrandDto>> GetAllBrandAsync();
        Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        Task CreateBrandAsync(CreateBrandDto createBrandDto);
        Task<GetBrandByIdDto> GetBrandByIdAsync(int id);
    }
}
