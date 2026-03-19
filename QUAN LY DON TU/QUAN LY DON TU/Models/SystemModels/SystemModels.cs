using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.SystemModels
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string CompanyName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string SubDomain { get; set; } = string.Empty; // hr.companyA.com
        [MaxLength(500)]
        public string LogoUrl { get; set; } = string.Empty;
        [MaxLength(7)]
        public string PrimaryColor { get; set; } = "#6366f1";
        [MaxLength(7)]
        public string SecondaryColor { get; set; } = "#8b5cf6";
        [MaxLength(30)]
        public string Plan { get; set; } = "Basic"; // Basic, Pro, Enterprise
        public int MaxUsers { get; set; } = 50;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ExpiresAt { get; set; }
    }

    public class TenantConfig
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Category { get; set; } = "General"; // General, Email, Notification, Leave, Theme
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
    }

    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [MaxLength(30)]
        public string Plan { get; set; } = "Basic";
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(30)]
        public string Status { get; set; } = "Active"; // Active, Expired, Cancelled
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime? LastPaymentDate { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant? Tenant { get; set; }
    }

    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Type { get; set; } = "Info"; // Info, Approval, Reminder, Escalation, System
        [MaxLength(500)]
        public string? ActionUrl { get; set; }
        public int? RelatedRequestId { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ReadAt { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class EmailTemplate
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty; // NewRequest, Approved, Rejected, Reminder, Escalation
        [Required, MaxLength(200)]
        public string Subject { get; set; } = string.Empty;
        public string BodyHtml { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class EmailLog
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [MaxLength(256)]
        public string ToEmail { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;
        [MaxLength(30)]
        public string Status { get; set; } = "Sent"; // Sent, Failed, Queued
        public string? ErrorMessage { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public int? RelatedRequestId { get; set; }
    }

    public class SystemError
    {
        [Key]
        public int Id { get; set; }
        public int? TenantId { get; set; }
        [MaxLength(200)]
        public string Source { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        [MaxLength(30)]
        public string Severity { get; set; } = "Error"; // Info, Warning, Error, Critical
        public DateTime OccurredAt { get; set; } = DateTime.Now;
        [MaxLength(50)]
        public string? IpAddress { get; set; }
        public int? UserId { get; set; }
    }
}
