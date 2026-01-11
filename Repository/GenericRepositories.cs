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
using System.ComponentModel;

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
            if (typeof(T) == typeof(School))
            {
                return (IReadOnlyList<T>)await _dbcontext.Set<School>().ToListAsync();
            }
            if (typeof(T) == typeof(School_Details))
            {
                return (IReadOnlyList<T>)await _dbcontext.Set<School_Details>().Include(p => p.School).ToListAsync();
            }
            return await _dbcontext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetById(int id)
        {
            if (typeof(T) == typeof(School))
            {
                return await _dbcontext.Set<School>().Where(p => p.ID == id).FirstOrDefaultAsync() as T;
            }
            if (typeof(T) == typeof(School_Details))
            {
                return await _dbcontext.Set<School_Details>().Where(p => p.ID == id).Include(p => p.School).FirstOrDefaultAsync() as T;
            }
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
        public async Task<T> AddAsync(T entity)
        {
            await _dbcontext.AddAsync(entity);
            return entity;
        } 
        public async Task<T> UpdateAsync(T entity)
        {
            _dbcontext.Update(entity);
            return entity;
        } 
        public void Delete(T entity)
        { 
            _dbcontext.Remove(entity);
        }   

    
    }
}
