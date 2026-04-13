using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Carola.DataAccessLayer.Repository;
using Carola.DtoLayer.Dtos.CategoryDtos;
using Carola.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DataAccessLayer.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly CarolaContext _context;

        public EfCategoryDal(CarolaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ResultCategoryDto>> GetCategoriesWithCarCountAsync()
        {
            return await _context.Categories
                .Select(x => new ResultCategoryDto
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.CategoryName,
                    CarCount = x.Cars.Count()
                })
                .ToListAsync();
        }
    }
}
