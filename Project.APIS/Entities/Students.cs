using Core.FingerId;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Students : BaseEntity
    {
        public string St_Code { get; set; }
        public string St_NameAr { get; set; }
        public string? St_NameEn { get; set; }
        public string St_Email { get; set; }
        public string? St_Image { get; set; }
        public string Phone { get; set; } 
        public int? FingerID { get; set; }  
        [ForeignKey(nameof(Faculty))] 
        public int Fac_Id { get; set; }
        public Faculty Faculty { get; set; }
        //********************************************************** 
        [ForeignKey(nameof(FacultyYear))] 
        public int FacYear_ID { get; set; }
        public FacultyYear FacultyYear { get; set; }
        //********************************************************** 
        [ForeignKey(nameof(FacultyYearSemister))]
        public int FacYearSem_ID { get; set; }
        public FacultyYearSemister FacultyYearSemister { get; set; }
        //********************************************************** 
        public ICollection<Studets_Subject> StudentRooms { get; set; }
    }
}
