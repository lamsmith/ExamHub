namespace ExamHub.DTO
{
    public class ExamResultResponseModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ExamId { get; set; }
        public string Subject { get; set; }
        public int Score { get; set; }
        public double Percentage { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
