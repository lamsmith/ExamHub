using System.ComponentModel.DataAnnotations.Schema;

namespace ExamHub.Entity
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int GeneralExamResultId { get; set; }
        public GeneralExamResult GeneralExamResult { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int Score { get; set; }
        public double Percentage { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
