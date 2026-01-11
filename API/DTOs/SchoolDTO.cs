namespace API.DTOs
{
    public class SchoolDTO
    {
        public int Id { get; set; } 
        public string SchoolName { get; set; } = null!;
        public string? ImageCover { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Status { get; set; }
    }
}
