using Core.Entities;
using Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.DTOs
{
    public class LectureStudentDto
    { 
        public int Lecture_ID { get; set; }  
        public int St_ID { get; set; } 

    }
}
