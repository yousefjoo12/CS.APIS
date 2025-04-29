using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Attendance : BaseEntity
    {
        public string Sub_Name { get; set; }
        public int LectureID { get; set; } 
        public Lecture Lecture { get; set; }
        public int St_ID { get; set; } 
        public Students Students { get; set; }
        public int Sub_ID { get; set; } 
        public Subjects Subjects { get; set; }
        public bool Atten { get; set; } 
    }
}
