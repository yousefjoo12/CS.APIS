using Core.Entities;

namespace API.DTOs
{
    public class LectureDTO
    {
        public int ID { get; set; }
        public string Lecture_Name { get; set; }  
        public int Sub_ID { get; set; }
        public string Subjects { get; set; }
        public DateTime LectureDate { get; set; }
        public int Degree { get; set; }

    }
}
