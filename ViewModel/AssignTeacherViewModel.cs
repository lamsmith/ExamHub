
using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class AssignTeacherViewModel
    {
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<TeacherResponseModel> Teachers { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
        public IEnumerable<SubjectResponseModel> Subjects { get; set; }
    }
}
