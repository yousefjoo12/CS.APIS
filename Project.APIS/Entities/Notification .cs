using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Notification : BaseEntity
    {  
        public string Title { get; set; }
        public string Massage { get; set; }
        //********************************************
        [ForeignKey(nameof(FacultyYearSemister))]
        public int FacYearSem_ID { get; set; }
        public FacultyYearSemister FacultyYearSemister { get; set; }
    }
}
