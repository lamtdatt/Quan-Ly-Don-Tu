using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.HR
{
    public class JobRequisition
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public int Headcount { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? BudgetMin { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BudgetMax { get; set; }

        [MaxLength(30)]
        public string Status { get; set; } = "Pending";

        public int? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("DepartmentId")]
        public virtual Organization.Department? Department { get; set; }
        [ForeignKey("JobTitleId")]
        public virtual Organization.JobTitle? JobTitle { get; set; }
        [ForeignKey("CreatedByUserId")]
        public virtual Organization.User? CreatedBy { get; set; }
    }

    public class JobRequisitionApproval
    {
        [Key]
        public int Id { get; set; }
        public int JobRequisitionId { get; set; }
        public int? ApproverId { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
        public DateTime? ActionDate { get; set; }
        [MaxLength(500)]
        public string? Comments { get; set; }

        [ForeignKey("JobRequisitionId")]
        public virtual JobRequisition? JobRequisition { get; set; }
        [ForeignKey("ApproverId")]
        public virtual Organization.User? Approver { get; set; }
    }

    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Source { get; set; } = "Referral";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class CandidateApplication
    {
        [Key]
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int JobRequisitionId { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.Now;
        [MaxLength(20)]
        public string Status { get; set; } = "Applied";

        [ForeignKey("CandidateId")]
        public virtual Candidate? Candidate { get; set; }
        [ForeignKey("JobRequisitionId")]
        public virtual JobRequisition? JobRequisition { get; set; }
    }

    public class InterviewSchedule
    {
        [Key]
        public int Id { get; set; }
        public int CandidateApplicationId { get; set; }
        public int? InterviewerId { get; set; }
        public DateTime ScheduledAt { get; set; }
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;
        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled";
        [MaxLength(500)]
        public string? Notes { get; set; }

        [ForeignKey("CandidateApplicationId")]
        public virtual CandidateApplication? CandidateApplication { get; set; }
        [ForeignKey("InterviewerId")]
        public virtual Organization.User? Interviewer { get; set; }
    }

    public class OfferLetter
    {
        [Key]
        public int Id { get; set; }
        public int CandidateApplicationId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OfferedSalary { get; set; }
        public DateTime StartDate { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Draft";
        public DateTime? SentAt { get; set; }

        [ForeignKey("CandidateApplicationId")]
        public virtual CandidateApplication? CandidateApplication { get; set; }
    }
}
