using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.studetsSpecifications
{
    public class studetsSpecParams
    {
        public string? sort { get; set; }
        //public int? BrandId { get; set; }
        //public int? CategoryId { get; set; }

        private const int MaxPageSize = 10;
        private int pageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Search { get; set; }

    }
}
