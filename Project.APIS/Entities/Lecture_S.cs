using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Lecture_S : BaseEntity
    { 
        public string Lecture_Num { get; set; }
       // public Students Students { get; set; }
        public int Room_ID { get; set; } 
       // public Rooms Rooms { get; set; } 
        public int Sub_ID { get; set; } 
      //  public Subjects Subjects { get; set; } 
        public DateTime LectureDate { get; set; } 
        public int Degree { get; set; } 
    }
}
