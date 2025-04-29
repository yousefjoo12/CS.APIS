using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Subjects : BaseEntity
    {
        public string Sub_Name { get; set; } 
        public int Dr_ID { get; set; } 
        public Doctors Doctors { get; set; }  
        public int Ins_ID { get; set; } 
        public Instructors Instructors { get; set; } 
        public int FacYearSem_ID { get; set; } 
        public FacultyYearSemister FacultyYearSemister { get; set; } 
    }
}
