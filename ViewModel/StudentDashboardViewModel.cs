using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class StudentDashboardViewModel
    {
        public IEnumerable<ExamResponseModel> UpcomingExams { get; set; }
      //  public IEnumerable<ExamResult> RecentResults { get; set; }
        public IEnumerable<NotificationResponseModel> Notifications { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
      
        public string Name { get; set; }
        public string StudentId { get; set; }

    }
}

