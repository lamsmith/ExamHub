using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class SubjectResponseModel
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int CreatedByPrincipalId { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Class> Class { get; set; }
        public ICollection<Exam> Exams { get; set; }
    }
}
