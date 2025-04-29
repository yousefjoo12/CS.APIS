using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Lecture : BaseEntity
    {
        public string Sub_Name { get; set; } 
        public int Degree { get; set; } 
        public int St_ID { get; set; } 
        public int Room_ID { get; set; } 
        public int Sub_ID { get; set; } 
    }
}
