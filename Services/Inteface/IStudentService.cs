using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Enum;

namespace ExamHub.Services.Inteface
{
    public interface IStudentService
    {
        IEnumerable<ExamResponseModel> GetAvailableExams(string studentName);
        IEnumerable<StudentResponseModel> GetAllStudent();
        public int GetTotalStudents();
        IEnumerable<StudentResponseModel> GetStudentsByClass(int classId);
        IEnumerable<StudentResponseModel> GetStudents(int teacherId);       
        Student GetStudentById(int studentId);
        IEnumerable<ExamResponseModel> GetUpcomingExams(int studentId);
        public void CreateStudent(CreateStudentRequestModel model);
        public void AssignStudentToClass(int studentId, int classId);
        public Student GetStudentByUserId(int userId);
        public IEnumerable<int> GetStudentAnswersForExam(int studentId, int examId);
    }
}
