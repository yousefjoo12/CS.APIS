using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FingerId
{
    public class SensorData :BaseEntity
    {  
        public int FingerID { get; set; }  
        public DateTime Timestamp { get; set; }

    }
}
