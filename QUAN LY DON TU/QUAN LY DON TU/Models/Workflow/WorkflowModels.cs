using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.Workflow
{
    public class FormTemplate
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? EnglishName { get; set; } // Optional English display name
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty; // Leave, OT, Travel, Expense, Equipment, Custom
        [MaxLength(50)]
        public string Icon { get; set; } = "bi-file-text";
        [MaxLength(20)]
        public string IconColor { get; set; } = "#6366f1";
        public int? WorkflowId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool RequiresFinancialApproval { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual WorkflowDef? Workflow { get; set; }
        public virtual ICollection<FormField> Fields { get; set; } = new List<FormField>();
    }

    public class FormField
    {
        [Key]
        public int Id { get; set; }
        public int FormTemplateId { get; set; }
        [Required, MaxLength(100)]
        public string Label { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string FieldName { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        public string FieldType { get; set; } = "Text"; // Text, Number, Date, DateRange, Dropdown, Textarea, FileUpload, Checkbox
        public bool IsRequired { get; set; } = false;
        public int DisplayOrder { get; set; } = 0;
        [MaxLength(500)]
        public string? Placeholder { get; set; }
        [MaxLength(1000)]
        public string? ValidationRules { get; set; } // JSON: {"min":0,"max":100}
        [MaxLength(500)]
        public string? DefaultValue { get; set; }
        public int? Width { get; set; } = 12; // Bootstrap grid 1-12
        public virtual FormTemplate? FormTemplate { get; set; }
        public virtual ICollection<FormFieldOption> Options { get; set; } = new List<FormFieldOption>();
    }

    public class FormFieldOption
    {
        [Key]
        public int Id { get; set; }
        public int FormFieldId { get; set; }
        [Required, MaxLength(200)]
        public string Label { get; set; } = string.Empty;
        [Required, MaxLength(200)]
        public string Value { get; set; } = string.Empty;
        public int DisplayOrder { get; set; } = 0;
        public virtual FormField? FormField { get; set; }
    }

    public class WorkflowDef
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();
    }

    public class WorkflowStep
    {
        [Key]
        public int Id { get; set; }
        public int WorkflowId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public int StepOrder { get; set; } = 1;
        [MaxLength(50)]
        public string ApproverType { get; set; } = "DirectManager"; // DirectManager, Role, SpecificUser, DepartmentHead
        public int? ApproverUserId { get; set; }
        public int? ApproverRoleId { get; set; }
        public bool CanSkipIfApplicant { get; set; } = false; // Skip step if requester holds this role
        public bool IsActive { get; set; } = true;
        public virtual WorkflowDef? Workflow { get; set; }
        public virtual ICollection<WorkflowCondition> Conditions { get; set; } = new List<WorkflowCondition>();
        public virtual ICollection<WorkflowStepApprover> Approvers { get; set; } = new List<WorkflowStepApprover>();
    }

    public class WorkflowCondition
    {
        [Key]
        public int Id { get; set; }
        public int WorkflowStepId { get; set; }
        [MaxLength(100)]
        public string FieldName { get; set; } = string.Empty; // Form field to check
        [MaxLength(20)]
        public string Operator { get; set; } = "GreaterThan"; // GreaterThan, LessThan, Equals, Contains
        [MaxLength(200)]
        public string Value { get; set; } = string.Empty; // Threshold value
        public int? NextStepId { get; set; } // Branch to this step if condition met
        public virtual WorkflowStep? WorkflowStep { get; set; }
    }

    public class WorkflowStepApprover
    {
        [Key]
        public int Id { get; set; }
        public int WorkflowStepId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public virtual WorkflowStep? WorkflowStep { get; set; }
    }

    public class Delegation
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int DelegatorId { get; set; } // Original approver
        public int DelegateId { get; set; } // Person receiving delegation
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey("DelegatorId")]
        public virtual Organization.User? Delegator { get; set; }
        [ForeignKey("DelegateId")]
        public virtual Organization.User? Delegate { get; set; }
    }

    public class SlaConfig
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int? FormTemplateId { get; set; }
        public int ReminderHours { get; set; } = 24;
        public int EscalationHours { get; set; } = 48;
        public bool AutoRemind { get; set; } = true;
        public bool AutoEscalate { get; set; } = true;
        public virtual FormTemplate? FormTemplate { get; set; }
    }

    public class EscalationRule
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int SlaConfigId { get; set; }
        public int EscalateToUserId { get; set; }
        public int EscalateAfterHours { get; set; } = 48;
        [MaxLength(500)]
        public string NotificationMessage { get; set; } = string.Empty;
        public virtual SlaConfig? SlaConfig { get; set; }
        [ForeignKey("EscalateToUserId")]
        public virtual Organization.User? EscalateToUser { get; set; }
    }
}
