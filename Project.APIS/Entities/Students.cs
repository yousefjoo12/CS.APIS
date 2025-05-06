using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Students : BaseEntity
    {
        public string St_Code { get; set; }
        public string St_NameAr { get; set; }
        public string St_NameEn { get; set; }
        public string St_Image { get; set; }
        public string Phone { get; set; }  
        public int Fac_ID { get; set; }
        public Faculty Faculty { get; set; }
        public int FacYearSem_ID { get; set; }
        public FacultyYearSemister FacultyYearSemister { get; set; }
    }
}
