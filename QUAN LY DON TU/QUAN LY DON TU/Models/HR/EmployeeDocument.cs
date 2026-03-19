using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.HR
{
    public class EmployeeDocument
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        
        [Required, MaxLength(100)]
        public string DocumentName { get; set; } = string.Empty; // CMND, CCCD, Hợp đồng, Bằng cấp...
        
        [MaxLength(50)]
        public string DocumentType { get; set; } = "Identification"; // Identity, Contract, Education, Other
        
        [MaxLength(500)]
        public string FileUrl { get; set; } = string.Empty;
        
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }
}
