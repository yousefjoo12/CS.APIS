using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public enum Days
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6,
        Saturday = 7
    }
    public class Lecture_S : BaseEntity
    {
        public string Lecture_Name { get; set; }

        [ForeignKey(nameof(Subjects))] 
        public int Sub_ID { get; set; }
        public Subjects Subjects { get; set; }
        public DateTime LectureDate { get; set; }
        public int? Degree { get; set; } 
    }
}
