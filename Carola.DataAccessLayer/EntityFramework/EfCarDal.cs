using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Carola.DataAccessLayer.Repository;
using Carola.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {
        public EfCarDal(CarolaContext context) : base(context)
        {
        }

        public async Task<List<Car>> GetAllCarsWithCategoryAsync()
        {
            var values = await Context.Cars.Include(c => c.Category).ToListAsync();
            return values;
        }
    }
}
