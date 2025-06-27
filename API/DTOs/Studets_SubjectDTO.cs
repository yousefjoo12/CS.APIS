using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{ 
    public class Studets_SubjectDTO
    {
        public int ID { get; set; }
        public int St_ID { get; set; } 
        public int Sub_ID { get; set; } 
    }
}
