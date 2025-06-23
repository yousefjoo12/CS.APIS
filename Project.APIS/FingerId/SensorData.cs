using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FingerId
{
    public class SensorData  
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FingerID { get; set; }  
        public DateTime Timestamp { get; set; }

    }
}
