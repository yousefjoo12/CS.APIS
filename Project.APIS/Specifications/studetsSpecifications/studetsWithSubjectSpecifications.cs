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
        public studetsWithSubjectSpecifications(studetsSpecParams spec) : base(P=>
             (string.IsNullOrEmpty(spec.Search) || P.St_NameAr.ToLower().Contains(spec.Search.ToLower()))
        // && (!spec.BrandId.HasValue || P.BrandId == spec.BrandId.Value) &&
        //(!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId.Value)
            )
        {
            if (!string.IsNullOrEmpty(spec.sort))
            {
                switch (spec.sort)
                {
                    case "CodeAsc":
                        // orderby(P=>p.St_Code)
                        AddOrderBy(P => P.St_Code);
                        break;
                    case "CodeDcse":
                        AddOrderByDecs(P => P.St_Code);
                        // orderbyDecs(P=>p.St_Code)
                        break;
                    default:
                        AddOrderBy(P => P.St_NameAr);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.ID);
            }
        }
        public studetsWithSubjectSpecifications(int id) : base(P => P.ID == id)
        {

        }
    }
}
