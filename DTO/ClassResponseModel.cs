using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class ClassResponseModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public ICollection<TeacherResponseModel> Teachers { get; set; } = new List<TeacherResponseModel>();
        public ICollection<StudentResponseModel> Students { get; set; } = new List<StudentResponseModel>();
        public ICollection<SubjectResponseModel> Subjects { get; set; } = new List<SubjectResponseModel>();
        public ICollection<ExamResponseModel> Exams { get; set; } = new List<ExamResponseModel>();
    }

}
