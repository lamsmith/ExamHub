using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class ExamsViewModel
    {
        public List<ExamViewModel> Exams { get; set; }
    }

    public class ExamViewModel
    {
        public int ExamId { get; set; }
        public string Class { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
