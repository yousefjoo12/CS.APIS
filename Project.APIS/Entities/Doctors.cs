using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Doctors : BaseEntity
    { 
        public string Dr_Code { get; set; }
        public string Dr_NameAr { get; set; }
        public string? Dr_NameEn { get; set; }
        public string Dr_Email { get; set; }
        public string? Dr_Image { get; set; }
        public string? Phone { get; set; }
        //***************************************
        [ForeignKey(nameof(Faculty))]
        public int Fac_ID { get; set; }
        public Faculty Faculty { get; set; }

    }
}
