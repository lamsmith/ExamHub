using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class AssignSubjectViewModel
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
        public IEnumerable<SubjectResponseModel> Subjects { get; set; }
    }
}
