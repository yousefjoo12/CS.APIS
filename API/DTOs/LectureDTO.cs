using Core.Entities;
using Core.Enums;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace API.DTOs
{
    public class LectureDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Subject ID is required")]
        public int Sub_ID { get; set; }

        public string? Subjects { get; set; } // للعرض فقط

        [Required(ErrorMessage = "Day is required")]
        [Range(1, 6, ErrorMessage = "Day must be between 1 (Saturday) and 6 (Thursday)")]
        public int Day { get; set; }

        [Required(ErrorMessage = "FromTime is required")]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d:[0-5]\d$", ErrorMessage = "FromTime must be in HH:mm:ss format")]
        public string FromTime { get; set; }

        [Required(ErrorMessage = "ToTime is required")]
        [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d:[0-5]\d$", ErrorMessage = "ToTime must be in HH:mm:ss format")]
        public string ToTime { get; set; }
    }
}
