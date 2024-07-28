using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class StudentResponseModel
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public string FristName { get; set; }   
        public string LastName { get; set; }
        public ICollection<Class> Class { get; set; } = new List<Class>();
        public ICollection<Subject> Subject { get; set; } = new List<Subject>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();

    }
}
