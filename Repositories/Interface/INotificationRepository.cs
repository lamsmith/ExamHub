using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId);
        Task<Notification> GetNotificationByIdAsync(int id);
        Task AddNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(int id);
        IEnumerable<Notification> GetNotificationsForStudent(int studentId);
    }
}