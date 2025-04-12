using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FacultyYear : BaseEntity
    {
        [ForeignKey("Fac_ID")] 
        public int Fac_ID { get; set; }


        [InverseProperty("FacultyYear")]
        public Faculty Faculty { get; set; }


        [InverseProperty("FacultyYearSemister")]
        public ICollection<FacultyYearSemister> FacultyYearSemister { get; set; } = new HashSet<FacultyYearSemister>();

    }
}
