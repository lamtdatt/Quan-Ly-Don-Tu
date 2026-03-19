using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.HR
{
    public class SalaryAdjustmentRequest
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int? RequestedByUserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProposedSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("RequestedByUserId")]
        public virtual Organization.User? RequestedBy { get; set; }
    }

    public class BonusRequest
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int? RequestedByUserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [MaxLength(50)]
        public string Type { get; set; } = "Spot";
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("RequestedByUserId")]
        public virtual Organization.User? RequestedBy { get; set; }
    }
}
