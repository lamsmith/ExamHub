using ExamHub.DTO;
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
        IEnumerable<Exam> GetExamsForStudent(int classId);
        IEnumerable<Exam> GetUpcomingExamsByClass(int classId);
        public void SaveExamResult(ExamResult examResult);
        public List<ExamQuestion> GetExamQuestionsForExam(int examId);
        public List<StudentAnswer> GetStudentAnswersForExam(int studentId, int examId);
        public IEnumerable<StudentAnswerResponseModel> GetSelectedOptionTextsForExam(int examId, int studentId);
        // IEnumerable<ExamResult> GetRecentExamResults(int studentId);
        ExamQuestion GetExamQuestionById(int id);
        void UpdateExamQuestion(ExamQuestion examQuestion);
        void DeleteExamQuestion(int id);

    }
}
