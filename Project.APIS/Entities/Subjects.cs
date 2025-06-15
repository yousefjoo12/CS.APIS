using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Subjects : BaseEntity
    {
        public string Sub_Code { get; set; }
        public string Sub_Name { get; set; }
        [ForeignKey(nameof(Doctors))] 
        public int Dr_ID { get; set; }
        public Doctors Doctors { get; set; }
        [ForeignKey(nameof(FacultyYearSemister))] 
        public int FacYearSem_ID { get; set; }
        public FacultyYearSemister FacultyYearSemister { get; set; }

        [ForeignKey(nameof(Rooms))]
        public int Room_ID { get; set; }
        public Rooms Rooms { get; set; }
        public ICollection<Studets_Subject> Studets_Subject { get; set; }

    }
}
