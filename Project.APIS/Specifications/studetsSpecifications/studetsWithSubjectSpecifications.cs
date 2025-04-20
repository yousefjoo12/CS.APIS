using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specifications;

namespace Core.Specifications.studetsSpecifications
{
    public class studetsWithSubjectSpecifications : BaseSpecifications<Students>
    {
        public studetsWithSubjectSpecifications() : base()
        {

        }
        public studetsWithSubjectSpecifications(int id) : base(P => P.ID == id)
        {

        }
    }
}
