using Core.Entities;
using Core.Repositories.Contract;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepositories(StoreContext dbcontext)
        {
          _dbcontext = dbcontext;
        }
        public Task<IReadOnlyList<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
