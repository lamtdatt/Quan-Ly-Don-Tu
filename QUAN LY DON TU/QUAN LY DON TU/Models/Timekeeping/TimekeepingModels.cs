using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DANGCAPNE.Models.Timekeeping
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Annual, Sick, Maternity, Unpaid, Compensatory
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;
        public double DefaultDaysPerYear { get; set; } = 12;
        public bool AllowCarryOver { get; set; } = false;
        public int CarryOverMaxDays { get; set; } = 0;
        public int CarryOverExpiryMonth { get; set; } = 3; // Month limit for carry-over
        public bool IsPaid { get; set; } = true;
        public bool AllowNegativeBalance { get; set; } = false;
        [MaxLength(20)]
        public string IconColor { get; set; } = "#10b981";
        public bool IsActive { get; set; } = true;
    }

    public class LeaveBalance
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int LeaveTypeId { get; set; }
        public int Year { get; set; }
        public double TotalEntitled { get; set; } = 12;
        public double Used { get; set; } = 0;
        public double CarriedOver { get; set; } = 0;
        public double SeniorityBonus { get; set; } = 0;
        public double CompensatoryDays { get; set; } = 0;
        public double Remaining => TotalEntitled + CarriedOver + SeniorityBonus + CompensatoryDays - Used;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("LeaveTypeId")]
        public virtual LeaveType? LeaveType { get; set; }
    }

    public class LeaveAccrual
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int LeaveTypeId { get; set; }
        public double Days { get; set; }
        [MaxLength(50)]
        public string AccrualType { get; set; } = "Monthly"; // Monthly, Annual, Seniority, CompOff, CarryOver
        [MaxLength(500)]
        public string? Description { get; set; }
        public DateTime AccrualDate { get; set; } = DateTime.Now;
        public int? RelatedRequestId { get; set; } // Link to OT request for comp-off
    }

    public class Holiday
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsRecurring { get; set; } = true;
        [MaxLength(50)]
        public string Country { get; set; } = "VN";
    }

    public class Shift
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty; // Morning, Afternoon, Night, FullDay
        [MaxLength(20)]
        public string Code { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }
        public int GracePeriodMinutes { get; set; } = 15;
        public bool IsActive { get; set; } = true;
    }

    public class UserShift
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
        [ForeignKey("ShiftId")]
        public virtual Shift? Shift { get; set; }
    }

    public class Timesheet
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        [MaxLength(30)]
        public string Source { get; set; } = "Manual"; // Fingerprint, FaceRecognition, GPS, Manual, QRCode, Wifi
        public double? GpsLatitude { get; set; }
        public double? GpsLongitude { get; set; }
        [MaxLength(100)]
        public string? WifiName { get; set; }
        [MaxLength(30)]
        public string? WifiBssid { get; set; }
        [MaxLength(100)]
        public string? QrCodeKey { get; set; }
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }
        [MaxLength(30)]
        public string Status { get; set; } = "Present"; // Present, Absent, Late, EarlyLeave, Leave, Holiday, OT
        public double WorkHours { get; set; } = 0;
        public double OtHours { get; set; } = 0;
        [MaxLength(500)]
        public string? Notes { get; set; }
        public bool IsValidGps { get; set; } = true;
        public bool IsValidWifi { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [ForeignKey("UserId")]
        public virtual Organization.User? User { get; set; }
    }

    public class DailyAttendance
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int? ShiftId { get; set; }
        public DateTime? ActualCheckIn { get; set; }
        public DateTime? ActualCheckOut { get; set; }
        public bool IsLate { get; set; } = false;
        public int LateMinutes { get; set; } = 0;
        public bool IsEarlyLeave { get; set; } = false;
        public int EarlyLeaveMinutes { get; set; } = 0;
        public double EffectiveHours { get; set; } = 0;
        [MaxLength(30)]
        public string Status { get; set; } = "Normal"; // Normal, Late, EarlyLeave, Absent, OnLeave, Holiday, BusinessTrip
        public bool HasApprovedLeave { get; set; } = false;
        public bool HasApprovedOutside { get; set; } = false;
        public DateTime CalculatedAt { get; set; } = DateTime.Now;
    }

    public class OvertimeRate
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty; // Weekday, Weekend, Holiday
        public double Multiplier { get; set; } = 1.5; // 1.5x, 2.0x, 3.0x
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class AttendanceLocationConfig
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        
        [MaxLength(100)]
        public string? WifiName { get; set; }
        [MaxLength(100)]
        public string? WifiBssid { get; set; }
        [MaxLength(100)]
        public string? QrCodeKey { get; set; }
        
        public double? AllowedLatitude { get; set; }
        public double? AllowedLongitude { get; set; }
        public int AllowedRadiusMeters { get; set; } = 100;

        public bool RequirePhoto { get; set; } = false;
        public bool IsActive { get; set; } = true;

        [ForeignKey("BranchId")]
        public virtual Organization.Branch? Branch { get; set; }
    }

    public class ShiftSwapRequest
    {
        [Key]
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int RequesterId { get; set; }
        public int TargetUserId { get; set; }
        public int RequesterShiftId { get; set; } // Ngày/Ca của người yêu cầu
        public DateTime RequesterDate { get; set; }
        public int TargetShiftId { get; set; }    // Ngày/Ca muốn đổi sang
        public DateTime TargetDate { get; set; }
        
        [MaxLength(500)]
        public string? Reason { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Completed
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? ApprovedByManagerId { get; set; }

        [ForeignKey("RequesterId")]
        public virtual Organization.User? Requester { get; set; }
        [ForeignKey("TargetUserId")]
        public virtual Organization.User? TargetUser { get; set; }
    }
}
