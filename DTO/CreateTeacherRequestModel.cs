using ExamHub.Enum;

namespace ExamHub.DTO
{
    public class CreateTeacherRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
    }
}
