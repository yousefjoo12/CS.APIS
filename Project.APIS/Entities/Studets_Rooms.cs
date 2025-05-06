using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Studets_Rooms : BaseEntity
    {
        public int St_ID { get; set; }
        public Students Students { get; set; } 
        public int Room_ID { get; set; } 
        public Rooms Rooms { get; set; }

    }
}
