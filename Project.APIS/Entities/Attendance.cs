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
        public int St_ID { get; set; } 
        public int Sub_ID { get; set; } 
        public bool Atten { get; set; } 
    }
}
