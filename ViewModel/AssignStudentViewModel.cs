using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class AssignStudentViewModel
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public IEnumerable<StudentResponseModel> Students { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
    }
}
