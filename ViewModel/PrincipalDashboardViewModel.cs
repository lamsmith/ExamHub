using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class PrincipalDashboardViewModel
    {
  
         public string PrincipalName { get; set; }
        public int TotalTeachers { get; set; }
        public int TotalStudents { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }  = new List<ClassResponseModel>();
        public IEnumerable<StudentResponseModel> Students { get; set; }
         public IEnumerable<ClassTeacher> ClassTeachers { get; set; }
        public IEnumerable<SubjectTeacher> SubjectTeachers { get; set; }
    }

}
