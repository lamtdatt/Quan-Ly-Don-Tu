using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.AdminOps
{
    public class AssetAssignment
    {
        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;
        public DateTime? ReturnedAt { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Assigned";

        [ForeignKey("AssetId")]
        public virtual Finance.Asset? Asset { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class AssetIncident
    {
        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int? ReportedByUserId { get; set; }
        [MaxLength(30)]
        public string Type { get; set; } = "Damage";
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; } = DateTime.Now;
        [MaxLength(20)]
        public string Status { get; set; } = "Open";

        [ForeignKey("AssetId")]
        public virtual Finance.Asset? Asset { get; set; }
        [ForeignKey("ReportedByUserId")]
        public virtual Organization.User? ReportedBy { get; set; }
    }

    public class CarBooking
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [MaxLength(200)]
        public string PickupLocation { get; set; } = string.Empty;
        [MaxLength(200)]
        public string Destination { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";
        [MaxLength(100)]
        public string? DriverName { get; set; }

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class MealRegistration
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(20)]
        public string MealType { get; set; } = "Overtime";
        [MaxLength(200)]
        public string? Notes { get; set; }

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class UniformRequest
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        [MaxLength(10)]
        public string Size { get; set; } = "M";
        public int Quantity { get; set; } = 1;
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";
        public DateTime RequestedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }
}
