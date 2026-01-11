using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class School_Details : BaseEntity
    { 
        public string? ImageCover { get; set; } 
        public string? Images { get; set; } 
        public string Description { get; set; } = null!; 
        public DateTime? CreatedAt { get; set; } 
        public string? CreatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? UpdatedBy { get; set; } 
        public bool Status { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; } 
        public School School { get; set; }
    }
}
