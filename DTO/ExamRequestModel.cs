namespace ExamHub.DTO
{
    public class ExamRequestModel
    {
        
        public string ExamName { get; set; }

        public int CreatedByTeacherId { get; set; }

        public int SubjectId { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int ClassId { get; set; }
    }
}
