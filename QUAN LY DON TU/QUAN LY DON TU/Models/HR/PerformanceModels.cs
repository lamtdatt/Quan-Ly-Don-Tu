using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.HR
{
    public class PerformanceCycle
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class PerformanceGoal
    {
        [Key]
        public int Id { get; set; }
        public int CycleId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; } = 1.0m;
        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        [ForeignKey("CycleId")]
        public virtual PerformanceCycle? Cycle { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class PerformanceReview
    {
        [Key]
        public int Id { get; set; }
        public int CycleId { get; set; }
        public int UserId { get; set; }
        public int? ReviewerId { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Draft";
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Score { get; set; }
        public DateTime? SubmittedAt { get; set; }

        [ForeignKey("CycleId")]
        public virtual PerformanceCycle? Cycle { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("ReviewerId")]
        public virtual Organization.User? Reviewer { get; set; }
    }

    public class PerformanceReviewItem
    {
        [Key]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int? GoalId { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Score { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set; }

        [ForeignKey("ReviewId")]
        public virtual PerformanceReview? Review { get; set; }
        [ForeignKey("GoalId")]
        public virtual PerformanceGoal? Goal { get; set; }
    }
}
