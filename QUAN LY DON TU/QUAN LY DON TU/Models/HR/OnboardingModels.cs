using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.HR
{
    public class OnboardingTaskTemplate
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Description { get; set; }
        public int DefaultDueDays { get; set; } = 7;
        public int? DefaultAssigneeRoleId { get; set; }

        [ForeignKey("DefaultAssigneeRoleId")]
        public virtual Organization.Role? DefaultAssigneeRole { get; set; }
    }

    public class OnboardingTask
    {
        [Key]
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int UserId { get; set; }
        public int? AssignedToUserId { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Open";
        public DateTime DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }

        [ForeignKey("TemplateId")]
        public virtual OnboardingTaskTemplate? Template { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("AssignedToUserId")]
        public virtual Organization.User? AssignedTo { get; set; }
    }

    public class OffboardingTaskTemplate
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Description { get; set; }
        public int DefaultDueDays { get; set; } = 7;
        public int? DefaultAssigneeRoleId { get; set; }

        [ForeignKey("DefaultAssigneeRoleId")]
        public virtual Organization.Role? DefaultAssigneeRole { get; set; }
    }

    public class OffboardingTask
    {
        [Key]
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int UserId { get; set; }
        public int? AssignedToUserId { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Open";
        public DateTime DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }

        [ForeignKey("TemplateId")]
        public virtual OffboardingTaskTemplate? Template { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("AssignedToUserId")]
        public virtual Organization.User? AssignedTo { get; set; }
    }
}
