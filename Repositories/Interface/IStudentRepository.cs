using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IStudentRepository
    {
        IEnumerable<Exam> GetAvailableExams(string studentName);
        Student GetStudentByName(string studentName);
        IEnumerable<Student> GetAllStudents();
        int GetTotalStudents();
        IEnumerable<Student> GetStudentsByClass(int classId);
        IEnumerable<Student> GetStudents(int teacherId);
        IEnumerable<Exam> GetUpcomingExams(int studentId);
        public Student GetStudentByUserId(int userId);
        Student GetStudentById(int studentId);
        IEnumerable<StudentExam> GetRecentExamResults(int studentId);
        void AddStudent(Student student);
        void AssignStudentToClass(int studentId, int classId);
        IEnumerable<int> GetSelectedOptionTextsForExam(int examId, int studentId);
        IEnumerable<StudentAnswer> GetStudentAnswersForExam(int studentId, int examId);


    }
}

