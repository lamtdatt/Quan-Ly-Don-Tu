using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.Compliance
{
    public class PolicyDocument
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Version { get; set; } = "1.0";
        public DateTime PublishedAt { get; set; }
        [MaxLength(300)]
        public string FileUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class PolicyAcknowledgement
    {
        [Key]
        public int Id { get; set; }
        public int PolicyDocumentId { get; set; }
        public int UserId { get; set; }
        public DateTime? AcknowledgedAt { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [ForeignKey("PolicyDocumentId")]
        public virtual PolicyDocument? PolicyDocument { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }
}
