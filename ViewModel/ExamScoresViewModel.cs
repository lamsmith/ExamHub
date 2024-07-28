namespace ExamHub.ViewModel
{
    public class ExamScoresViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public List<StudentExamScore> StudentExamScores { get; set; }
    }
        public class StudentExamScore
        {
            public int StudentId { get; set; }
            public string StudentName { get; set; }
            public int Score { get; set; }
        }
}
