using Core.Entities;
using Core.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Core.Specifications;
using Project.Repository;

namespace Repository
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepositories(StoreContext dbcontext)
        {
          _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAll()
        {
         return await _dbcontext.Set<T>().ToListAsync();
        } 
        public async Task<T?> GetById(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec).ToListAsync();
        } 
        public async Task<T?> GetWithspecAsync(ISpecifications<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>(), spec).FirstOrDefaultAsync();
        } 
    }
}
