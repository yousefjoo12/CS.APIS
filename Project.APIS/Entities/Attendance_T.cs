using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Attendance_T : BaseEntity
    { 
        public int LectureID { get; set; } 
        public Lecture_S Lecture { get; set; }
        public int St_ID { get; set; }
        public Students Students { get; set; }
        public bool Atten { get; set; } 
    }
}
