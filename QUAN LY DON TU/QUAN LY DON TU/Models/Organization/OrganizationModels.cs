using System.ComponentModel.DataAnnotations;

namespace DANGCAPNE.Models.Organization
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty; // Admin, Manager, HR, Employee
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? EnglishName { get; set; } // Optional English display name
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;
        public int? ParentDepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual Department? ParentDepartment { get; set; }
        public virtual User? Manager { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Address { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double AllowedRadius { get; set; } = 200; // meters
        [MaxLength(50)]
        public string TimeZone { get; set; } = "SE Asia Standard Time";
        public bool IsActive { get; set; } = true;
    }

    public class JobTitle
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 1;
        public bool IsActive { get; set; } = true;
    }

    public class Position
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual Department? Department { get; set; }
    }

    public class UserManager
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ManagerId { get; set; }
        public bool IsPrimary { get; set; } = true;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public virtual User? User { get; set; }
        public virtual User? Manager { get; set; }
    }

    public class Team
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public int? LeaderId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual User? Leader { get; set; }
        public virtual ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }

    public class TeamMember
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        [MaxLength(50)]
        public string Role { get; set; } = "Member"; // Leader, Member
        public DateTime JoinedAt { get; set; } = DateTime.Now;
        public virtual Team? Team { get; set; }
        public virtual User? User { get; set; }
    }
}
