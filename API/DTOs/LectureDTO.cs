using Core.Entities;
using Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.DTOs
{
    public class LectureDTO
    {
        public int ID { get; set; }
        public string? Lecture_Name { get; set; }  
        public int Sub_ID { get; set; }
        public string Subjects { get; set; }
        public Days day { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }

    }
}
