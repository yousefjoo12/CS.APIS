using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FacultyYearSemister : BaseEntity
    {  
        public string Sem_Code { get; set; }
        public string Sem_Name { get; set; }
        public int FacYear_ID { get; set; }
    }
}
