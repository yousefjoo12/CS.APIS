namespace API.DTOs
{
    public class School_DetailsDTO
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string School { get; set; }
        public string? ImageCover { get; set; }
        public string? Images { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public bool Status { get; set; }
    }
}
