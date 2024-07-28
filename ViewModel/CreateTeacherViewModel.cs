using ExamHub.DTO;
using ExamHub.Enum;

namespace ExamHub.ViewModel
{
    public class CreateTeacherViewModel
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }    
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
       // public string Subject { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<ClassResponseModel> Classes { get; set; }
        public IEnumerable<SubjectResponseModel> Subjects { get; set; }
    }
}
