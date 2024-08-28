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
        public List<int> ClassId { get; set; } = new();
        public List<int> SubjectId { get; set; } = new();
        public IEnumerable<ClassResponseModel> Classes { get; set; }
        public IEnumerable<SubjectResponseModel> Subjects { get; set; }
    }
}
