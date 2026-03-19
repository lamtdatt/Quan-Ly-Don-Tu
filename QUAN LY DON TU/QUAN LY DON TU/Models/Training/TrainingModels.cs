using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.Training
{
    public class TrainingCourse
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Provider { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class TrainingEnrollment
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Enrolled";
        public DateTime EnrolledAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }

        [ForeignKey("CourseId")]
        public virtual TrainingCourse? Course { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class Certification
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class CertificationRenewal
    {
        [Key]
        public int Id { get; set; }
        public int CertificationId { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.Now;
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovedAt { get; set; }

        [ForeignKey("CertificationId")]
        public virtual Certification? Certification { get; set; }
        [ForeignKey("ApprovedByUserId")]
        public virtual Organization.User? ApprovedBy { get; set; }
    }
}
