using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class TeacherDashboardViewModel
    {
        public string TeacherName { get; set; }
        public IEnumerable<ExamResponseModel> Exams { get; set; }
        public IEnumerable<TeacherResponseModel> Teachers { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
        public List<SubjectResponseModel> Subjects { get; set; }
        public int TeacherId { get; set; }
        public int CurrentClassId { get; set; }
    }
}

