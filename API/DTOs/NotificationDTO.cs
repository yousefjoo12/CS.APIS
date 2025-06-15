using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class NotificationDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Massage { get; set; } 
        public int FacYearSem_ID { get; set; }
        public string FacultyYearSemister { get; set; }
    }
}
