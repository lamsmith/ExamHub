using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ExamHub.Entity
{
    public class Student : Base
    {
        public int UserId { get; set; }
        public User User { get; set; }  
        public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
        public ICollection<SubjectStudent> SubjectStudents { get; set; } = new List<SubjectStudent>();
        public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}
