using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.Requests
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [MaxLength(30)]
        public string RequestCode { get; set; } = string.Empty; // Auto-generated: REQ-20260301-001
        public int FormTemplateId { get; set; }
        public int RequesterId { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(30)]
        public string Status { get; set; } = "Draft"; // Draft, Pending, InProgress, Approved, Rejected, Cancelled, RequestEdit
        public int CurrentStepOrder { get; set; } = 0;
        [MaxLength(30)]
        public string Priority { get; set; } = "Normal"; // Low, Normal, High, Urgent
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }

        [ForeignKey("FormTemplateId")]
        public virtual Workflow.FormTemplate? FormTemplate { get; set; }
        [ForeignKey("RequesterId")]
        public virtual Organization.User? Requester { get; set; }
        public virtual ICollection<RequestData> DataEntries { get; set; } = new List<RequestData>();
        public virtual ICollection<RequestAttachment> Attachments { get; set; } = new List<RequestAttachment>();
        public virtual ICollection<RequestApproval> Approvals { get; set; } = new List<RequestApproval>();
        public virtual ICollection<RequestComment> Comments { get; set; } = new List<RequestComment>();
        public virtual ICollection<RequestFollower> Followers { get; set; } = new List<RequestFollower>();
        public virtual ICollection<RequestAuditLog> AuditLogs { get; set; } = new List<RequestAuditLog>();
    }

    public class RequestData
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        [MaxLength(100)]
        public string FieldName { get; set; } = string.Empty;
        public string? FieldValue { get; set; }
        public virtual Request? Request { get; set; }
    }

    public class RequestAttachment
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        [MaxLength(256)]
        public string FileName { get; set; } = string.Empty;
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;
        [MaxLength(100)]
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public int UploadedById { get; set; }
        public virtual Request? Request { get; set; }
    }

    public class RequestApproval
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int StepOrder { get; set; }
        [MaxLength(100)]
        public string StepName { get; set; } = string.Empty;
        public int? ApproverId { get; set; }
        [MaxLength(30)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Skipped, Escalated
        [MaxLength(1000)]
        public string? Comments { get; set; }
        public DateTime? ActionDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool VerifiedByPin { get; set; } = false;
        [MaxLength(50)]
        public string? IpAddress { get; set; }
        public virtual Request? Request { get; set; }
        [ForeignKey("ApproverId")]
        public virtual Organization.User? Approver { get; set; }
    }

    public class RequestComment
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; }
        public virtual Request? Request { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        public virtual RequestComment? ParentComment { get; set; }
    }

    public class RequestFollower
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public DateTime FollowedAt { get; set; } = DateTime.Now;
        public virtual Request? Request { get; set; }
    }

    public class RequestAuditLog
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(50)]
        public string Action { get; set; } = string.Empty; // Created, Updated, Approved, Rejected, Escalated, Commented
        [MaxLength(30)]
        public string? OldStatus { get; set; }
        [MaxLength(30)]
        public string? NewStatus { get; set; }
        public string? Details { get; set; } // JSON details of changes
        [MaxLength(50)]
        public string? IpAddress { get; set; }
        [MaxLength(500)]
        public string? UserAgent { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual Request? Request { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class DraftRequest
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int FormTemplateId { get; set; }
        public string FormDataJson { get; set; } = "{}"; // Auto-saved JSON
        public DateTime LastSavedAt { get; set; } = DateTime.Now;
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("FormTemplateId")]
        public virtual Workflow.FormTemplate? FormTemplate { get; set; }
    }
}
