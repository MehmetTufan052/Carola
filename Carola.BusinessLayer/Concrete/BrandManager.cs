using Carola.BusinessLayer.Abstract;
using Carola.DataAccessLayer.Abstract;
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
        private readonly IValidator<Brand> _validator;

        public BrandManager(IBrandDal brandDal, IValidator<Brand> validator)
        {
            _brandDal = brandDal;
            _validator = validator;
        }

        public async Task TDeleteAsync(int id)
        {
            await _brandDal.DeleteAsync(id);
        }

        public async Task<List<Brand>> TGetAllAsync()
        {
            return await _brandDal.GetAllAsync();
        }

        public async Task<Brand> TGetByIdAsync(int id)
        {
            return await _brandDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Brand entity)
        {
            var result = await _validator.ValidateAsync(entity);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
           
            await _brandDal.InsertAsync(entity);    
        }

        public async Task TUpdateAsync(Brand entity)
        {
            await _brandDal.UpdateAsync(entity);
            
        }
    }
}
