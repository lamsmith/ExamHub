using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class ExamResponseModel

    {
        public int Id {  get; set; }
        public string ExamName { get; set; }
        public int CreatedByTeacherId { get; set; }
        public string  CreatedByTeacher { get; set; }
        public int SubjectId { get; set; }
        public DateTime DateTime { get; set; }
        public string  Subject { get; set; }
        public int ClassId { get; set; }
        public string  Class { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentResponseModel> Students { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
