using Microsoft.AspNetCore.SignalR;

namespace DANGCAPNE.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        }

        public async Task LeaveUserGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
        }

        public async Task SendNotification(string userId, string title, string message, string type, string? actionUrl)
        {
            await Clients.Group($"user_{userId}").SendAsync("ReceiveNotification", new
            {
                title,
                message,
                type,
                actionUrl,
                createdAt = DateTime.Now.ToString("HH:mm dd/MM/yyyy")
            });
        }

        public async Task RequestStatusChanged(string requesterId, string requestCode, string newStatus)
        {
            await Clients.Group($"user_{requesterId}").SendAsync("RequestStatusChanged", new
            {
                requestCode,
                newStatus,
                timestamp = DateTime.Now.ToString("HH:mm dd/MM/yyyy")
            });
        }
    }
}
