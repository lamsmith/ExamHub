namespace ExamHub.ViewModel
{
    //public class GeneralExamResultViewModel
    //{
    //    public int Id { get; set; }
    //    public int StudentId { get; set; }
    //    public string StudentName { get; set; }
    //    public double Percentage { get; set; }
    //    public List<ExamResultViewModel> ExamResults { get; set; }
    //}
    public class GeneralExamResultViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public double TotalPercentage { get; set; }
        public List<ExamResultViewModel> ExamResults { get; set; }
    }

    public class ExamResultViewModel
    {
        //public string ExamName { get; set; }
        //public DateTime ExamDate { get; set; }
        //public int Score { get; set; }
        //public double Percentage { get; set; }
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Subject { get; set; }
        public int Score { get; set; }
        public double Percentage { get; set; }
        public DateTime ExamDate { get; set; }

    }

}
