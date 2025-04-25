using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    { 
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // انا كدكد عرفته عشان ميضربش معايا null ref Ex
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDecs { get; set; }

        public BaseSpecifications()
        {
         // Critria = null
        }
        public BaseSpecifications(Expression<Func<T, bool>> CritriaExpression)
        {
            Critria = CritriaExpression;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDecs(Expression<Func<T, object>> Orderbydecs)
        {
            OrderByDecs = Orderbydecs;
        }
    }
}
