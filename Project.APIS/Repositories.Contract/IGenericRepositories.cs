using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specifications;

namespace Core.Repositories.Contract
{
    public interface IGenericRepositories<T> where T : BaseEntity
    {
        Task<T?> GetById(int id);
        Task<IReadOnlyList<T>> GetAll(); 

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec); 
        Task<T?> GetWithspecAsync(ISpecifications<T> spec);

    }
}
