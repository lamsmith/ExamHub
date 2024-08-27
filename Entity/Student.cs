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
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
        public ICollection<GeneralExamResult> GeneralExamResults { get; set; } = new List<GeneralExamResult>();
        public ICollection<StudentAnswer> StudentAnswers { get; set; } = new List<StudentAnswer>();
        public ICollection<ListAnswer> listanswers { get; set; } = new List<ListAnswer>();

    }
}
