using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamHub.Entity;
using ExamHub.Repositories.Implementations;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
    public class NotificationService : INotificationService
    {
         private readonly INotificationRepository _notificationRepository;
        private readonly IStudentRepository _studentRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int userId)
        {
            return await _notificationRepository.GetNotificationsByUserIdAsync(userId);
        }

        public async Task<Notification> GetNotificationAsync(int id)
        {
            return await _notificationRepository.GetNotificationByIdAsync(id);
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            notification.DateCreated = DateTime.UtcNow;
            notification.IsRead = false;
            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _notificationRepository.UpdateNotificationAsync(notification);
            }
        }

        public async Task DeleteNotificationAsync(int id)
        {
            await _notificationRepository.DeleteNotificationAsync(id);
        }

        public IEnumerable<StudentExam> GetRecentExamResults(int studentId)
        {
            return _studentRepository.GetRecentExamResults(studentId);
        }
    }
}