using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class StudentResponseModel
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Class> Class { get; set; } = new List<Class>();
        public ICollection<Subject> Subject { get; set; } = new List<Subject>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public string ClassName { get; set; } 

    }
}
