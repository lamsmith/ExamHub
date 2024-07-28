using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    internal class TeacherViewModel
    {
        public IEnumerable<TeacherResponseModel> Teachers { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
        public IEnumerable<SubjectResponseModel> Subjects  {get; set; }
        public IEnumerable<ClassTeacher> ClassTeachers {get; set;}
        public IEnumerable<SubjectTeacher> SubjectTeachers {get; set;}
        
        
    }
}