using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("FacYear_ID")]
        public int FacYear_ID { get; set; }

        [InverseProperty("FacultyYear")]
        public FacultyYear FacultyYear { get; set; }
    }
}
