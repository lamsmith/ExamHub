namespace ExamHub.DTO
{
    public class GeneralExamResultResponseModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public double Percentage { get; set; }
        public List<ExamResultResponseModel> ExamResults { get; set; }
    }
}
