using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.HR
{
    public class SocialInsurance
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        
        [MaxLength(20)]
        public string InsuranceNumber { get; set; } = string.Empty; // Số sổ BHXH
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryBasis { get; set; } // Mức lương đóng BH
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        [MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active, Suspended, Terminated
        
        [MaxLength(500)]
        public string? Note { get; set; }
        
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }
}
