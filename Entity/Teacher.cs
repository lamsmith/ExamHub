using System.ComponentModel.DataAnnotations.Schema;

namespace ExamHub.Entity
{
    public class Teacher :Base
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ClassTeacher> ClassTeachers { get; set; } = new List<ClassTeacher>();
        public ICollection<SubjectTeacher> SubjectTeachers { get; set; } = new List<SubjectTeacher>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
