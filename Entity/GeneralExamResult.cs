namespace ExamHub.Entity
{
    public class GeneralExamResult
    {
        public int Id { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
        public int StudentId { get; set; }  
        public Student Student { get; set; }
        public double Percentage { get; set; }

    }
}
