using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IExamRepository
    {
        void AddExam(Exam exam);
        IEnumerable<Exam> GetAllExams();
        IEnumerable<Exam> GetExamsByTeacherId(int teacherId);
        Exam GetExamById(int id);
        void UpdateExam(Exam exam);
        void DeleteExam(int id);
        public bool ExamExists(int examId);
        IEnumerable<Exam> GetExamsByTeacher(string teacherName);
        IEnumerable<StudentExam> GetExamScoresByExamId(int examId);
        IEnumerable<ExamQuestion> GetQuestionsByExamId(int examId);
        void SaveStudentAnswer(StudentAnswer studentAnswer);
        void SaveStudentExam(StudentExam studentExam);
        IEnumerable<Exam> GetExamsForStudent(int studentId);
        IEnumerable<Exam> GetUpcomingExamsByClass(int classId);
       // IEnumerable<ExamResult> GetRecentExamResults(int studentId);

    }
}
