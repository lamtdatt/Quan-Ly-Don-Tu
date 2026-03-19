using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.Security
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;

        [ForeignKey("RoleId")]
        public virtual Organization.Role? Role { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission? Permission { get; set; }
    }

    public class UserPermission
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PermissionId { get; set; }
        public int? GrantedByUserId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime GrantedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission? Permission { get; set; }
        [ForeignKey("GrantedByUserId")]
        public virtual Organization.User? GrantedBy { get; set; }
    }
}
