using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId);
        Task<Notification> GetNotificationAsync(int id);
        Task SendNotificationAsync(Notification notification);
        Task MarkAsReadAsync(int id);
        Task DeleteNotificationAsync(int id);
    }
}