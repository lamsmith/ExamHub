using ExamHub.DTO;

namespace ExamHub.ViewModel
{
    public class ClassStudentsViewModel
    {
        public List<StudentViewModel> Students { get; set; }
        //public IEnumerable<ClassResponseModel> Classes { get; set; }
        //public IEnumerable<StudentResponseModel> Students { get; set; }

    }

    public class StudentViewModel
    {
        public string FullName { get; set; }
        public string ClassName { get; set; }
    }
}
