using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class SetExamViewModel
    {
        public string ExamName { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
