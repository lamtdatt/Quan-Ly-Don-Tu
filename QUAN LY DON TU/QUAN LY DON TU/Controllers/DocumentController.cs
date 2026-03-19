using Microsoft.AspNetCore.Mvc;
using DANGCAPNE.Data;
using DANGCAPNE.Models.HR;
using DANGCAPNE.Services;
using Microsoft.EntityFrameworkCore;

namespace DANGCAPNE.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public DocumentController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadStaffDocument(int userId, string docName, string docType, IFormFile file, DateTime? expiryDate)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == null) return Json(new { success = false, message = "Vui lòng đăng nhập" });
            var tenantId = HttpContext.Session.GetInt32("TenantId") ?? 1;

            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "Vui lòng chọn file" });

            // Lưu file qua FileService
            var fileUrl = await _fileService.SaveFileAsync(file, "documents");

            var document = new EmployeeDocument
            {
                TenantId = tenantId,
                UserId = userId,
                DocumentName = docName,
                DocumentType = docType,
                FileUrl = fileUrl,
                ExpiryDate = expiryDate,
                CreatedAt = DateTime.Now
            };

            _context.EmployeeDocuments.Add(document);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã tải lên hồ sơ thành công", url = fileUrl });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var doc = await _context.EmployeeDocuments.FindAsync(id);
            if (doc == null) return Json(new { success = false, message = "Không tìm thấy hồ sơ" });

            _fileService.DeleteFile(doc.FileUrl);
            _context.EmployeeDocuments.Remove(doc);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã xóa hồ sơ" });
        }
    }
}
