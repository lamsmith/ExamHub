namespace ExamHub.Entity
{
    public class ListAnswer :Base
    {
        public ICollection<StudentAnswer> StudentAnswer { get; set; }
        public Student Student { get; set; }
        public Exam Exam { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }

        //public double CalculateScore()
        //{
        //    int score = 0;
        //    foreach (var item in StudentAnswer)
        //    {
                
        //    }
        //}
    }
}
