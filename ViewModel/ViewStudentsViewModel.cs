using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class ViewStudentsViewModel
    {
        public IEnumerable<StudentResponseModel> Students { get; set; }
        public int TeacherId { get; set; }
    }
}
