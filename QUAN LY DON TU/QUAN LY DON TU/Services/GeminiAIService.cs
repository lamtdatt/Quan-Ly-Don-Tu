using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace DANGCAPNE.Services
{
    public class GeminiAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private const string _geminiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=";

        public GeminiAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<string> AnalyzeDocumentAsync(string filePath, string contentType)
        {
            if (string.IsNullOrEmpty(_apiKey)) return "Chưa cấu hình Gemini API Key.";
            
            try
            {
                var fileBytes = await File.ReadAllBytesAsync(filePath);
                var base64File = Convert.ToBase64String(fileBytes);

                var payload = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new object[]
                            {
                                new { text = "Hãy phân tích chứng từ này. Trích xuất các thông tin quan trọng. Tính toàn vẹn của tài liệu có đáng tin cậy không, hay có dấu hiệu chỉnh sửa/sao chép/làm giả/photoshop không? Trả lời bằng tiếng Việt ngắn gọn, trình bày theo cấu trúc rõ ràng." },
                                new
                                {
                                    inline_data = new
                                    {
                                        mime_type = contentType,
                                        data = base64File
                                    }
                                }
                            }
                        }
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_geminiEndpoint}{_apiKey}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseString)!;
                    return $"🤖 **AI Assistant:**\n\n{result.candidates[0].content.parts[0].text}";
                }
                
                return $"🤖 **AI Assistant:** Lỗi khi gọi API AI ({(int)response.StatusCode}). Vui lòng kiểm tra lại cấu hình.";
            }
            catch (Exception ex)
            {
                return $"🤖 **AI Assistant:** Lỗi: {ex.Message}";
            }
        }

        public async Task<string> ParseRequestIntentAsync(string prompt, string templatesJson)
        {
             if (string.IsNullOrEmpty(_apiKey)) return "{}";
            
             var currentDate = DateTime.Now.ToString("dd/MM/yyyy");
             var systemPrompt = $@"Bạn là trợ lý ảo giúp nhân viên tạo đơn. Hôm nay là ngày {currentDate} (định dạng dd/MM/yyyy).
Dưới đây là danh sách các biểu mẫu (Form Templates) hiện có trong hệ thống dưới dạng JSON (mỗi field có FieldName, Label và FieldType):
{templatesJson}

Hãy đọc yêu cầu của nhân viên: '{prompt}'

Lưu ý: Mọi đơn đều luôn có thêm 2 trường mặc định là:
- FieldName: ""Title"" (Tiêu đề đơn, bạn tự sinh ra một chuyên nghiệp dựa theo yêu cầu. VD: ""Đơn xin nghỉ phép - Ốm 2 ngày"")
- FieldName: ""Priority"" (Mức độ ưu tiên, giá trị hợp lệ: ""Normal"", ""Low"", ""High"", ""Urgent"")

Quy tắc Xử lý Ngày tháng (QUAN TRỌNG):
- Đầu ra của tất cả các FieldType là ""Date"" BẮT BUỘC phải theo định dạng chuẩn HTML5: yyyy-MM-dd. Ví dụ: Nếu hôm nay là 12/03/2026, thì ghi là ""2026-03-12"".
- Nếu người dùng nói ""nghỉ 2 ngày từ hôm nay"": ""Từ ngày"" là hôm nay, ""Đến ngày"" tính bằng cách cộng (Số ngày - 1) vào ""Từ ngày"". Ví dụ nghỉ 2 ngày từ 12/03 thì ""Đến ngày"" là 13/03 (2026-03-13). Nghỉ 1 ngày thì Từ và Đến giống nhau.
- FieldName: ""Priority"" (Mức độ ưu tiên, giá trị hợp lệ: ""Normal"", ""Low"", ""High"", ""Urgent"")

Nhiệm vụ:
1. Xác định TemplateId (ID của biểu mẫu) phù hợp nhất.
2. Trích xuất thông tin để điền vào trường ""Title"" và ""Priority"".
3. Khớp thông tin từ yêu cầu vào càng nhiều FieldName của biểu mẫu càng tốt (bao gồm các loại nghỉ phép, ngày tháng, lý do, số tiền). Đảm bảo key trong FormData PHẢI TRÙNG KHỚP VỚI FieldName.
Trả về KẾT QUẢ DUY NHẤT LÀ MỘT CHUỖI JSON.
Cấu trúc JSON mẫu:
{{
  ""TemplateId"": 1,
  ""FormData"": {{
    ""Title"": ""Đơn xin nghỉ ốm 2 ngày"",
    ""Priority"": ""Normal"",
    ""LeaveType"": ""Ốm đau"",
    ""StartDate"": ""2023-10-25"",
    ""Reason"": ""Sốt siêu vi""
  }}
}}";
             
             try
             {
                 var payload = new
                 {
                     contents = new[]
                     {
                         new { parts = new[] { new { text = systemPrompt } } }
                     },
                     generationConfig = new { responseMimeType = "application/json" }
                 };

                 var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                 
                 int maxRetries = 3; // Thử lại tối đa 3 lần
                 for (int i = 0; i < maxRetries; i++)
                 {
                     var response = await _httpClient.PostAsync($"{_geminiEndpoint}{_apiKey}", content);

                     if (response.IsSuccessStatusCode)
                     {
                         var responseString = await response.Content.ReadAsStringAsync();
                         dynamic result = JsonConvert.DeserializeObject(responseString)!;
                         return result.candidates[0].content.parts[0].text.ToString().Replace("```json", "").Replace("```", "").Trim();
                     }
                     else if ((int)response.StatusCode == 429) // Lỗi TooManyRequests
                     {
                         Console.WriteLine($"[Cảnh báo] Quá tải API. Đang thử lại lần {i + 1}...");
                         // Đợi 2s ở lần 1, 4s ở lần 2, 6s ở lần 3
                         await Task.Delay(2000 * (i + 1)); 
                         continue; // Quay lại vòng lặp để gọi lại
                     }
                     else
                     {
                         // Các lỗi khác thì thoát luôn
                         return "{}"; 
                     }
                 }
                 return "{}"; // Thất bại sau 3 lần thử
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"[Gemini Exception] {ex.ToString()}");
                 return "{}";
             }
        }

        public async Task<string> GeneralChatAsync(List<ChatMessage> history)
        {
             if (string.IsNullOrEmpty(_apiKey)) return "Xin lỗi, hệ thống chưa được cấu hình API Key cho A.I.";

             var systemPrompt = @"Bạn là trợ lý ảo siêu thông minh, thân thiện của DANGCAPNE - Hệ thống Quản lý Đơn từ & Phê duyệt.
Nhiệm vụ của bạn là giải đáp thắc mắc, hướng dẫn sử dụng hệ thống, và hỗ trợ nhân viên các vấn đề chung một cách chuyên nghiệp. Mọi câu trả lời cần ngắn gọn, đi thẳng vào trọng tâm.";

             try
             {
                 var contentsList = new List<object>();
                 
                 // Thêm system prompt như một system instruction hoặc chèn vào đầu
                 contentsList.Add(new { role = "user", parts = new[] { new { text = systemPrompt } } });
                 contentsList.Add(new { role = "model", parts = new[] { new { text = "Vâng, tôi đã hiểu nhiệm vụ của mình." } } });

                 foreach (var msg in history)
                 {
                     string role = msg.Role == "ai" ? "model" : "user";
                     contentsList.Add(new { role = role, parts = new[] { new { text = msg.Text } } });
                 }

                 var payload = new
                 {
                     contents = contentsList.ToArray()
                 };

                 var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
                 var response = await _httpClient.PostAsync($"{_geminiEndpoint}{_apiKey}", content);

                 if (response.IsSuccessStatusCode)
                 {
                     var responseString = await response.Content.ReadAsStringAsync();
                     dynamic result = JsonConvert.DeserializeObject(responseString)!;
                     string reply = result.candidates[0].content.parts[0].text;
                     return reply.Trim();
                 }
                 else
                 {
                     var err = await response.Content.ReadAsStringAsync();
                     Console.WriteLine($"[Gemini Chat Error] {err}");
                     return "Có lúi húi chút lỗi xíu rồi, bạn thử lại nha.";
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"[Gemini Chat Exception] {ex.ToString()}");
                 return "Xin lỗi, hiện tại tôi không thể xử lý, vui lòng thử lại sau.";
             }
        }
    }

    public class ChatMessage
    {
        public string Role { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}