using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Faculty : BaseEntity
    { 
        public string Fac_Code { get; set; }
        public string Fac_Name { get; set; }
        public int Manger_ID { get; set; }
    }
}
