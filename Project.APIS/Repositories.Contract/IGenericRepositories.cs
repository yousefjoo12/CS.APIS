using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Contract
{
    public interface IGenericRepositories<T> where T : BaseEntity
    {
        Task<T?> GetById(int id);
        Task<IReadOnlyList<T>> GetAll(); 


    }
}
