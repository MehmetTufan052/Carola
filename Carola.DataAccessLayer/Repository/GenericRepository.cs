using Carola.DataAccessLayer.Abstract;
using Carola.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carola.DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        protected readonly CarolaContext Context;

        public GenericRepository(CarolaContext context)
        {
            Context = context;
        }

        public async Task DeleteAsync(int id)
        {
           var value = await Context.Set<T>().FindAsync(id);
            Context.Set<T>().Remove(value);
            await Context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await Context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
           await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);
            return Context.SaveChangesAsync();
        }
    }
}
