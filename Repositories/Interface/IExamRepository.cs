using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Implementation;

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
        public IEnumerable<ExamQuestion> GetExamQuestionsForExam(int examId);
        public IEnumerable<StudentAnswer> GetStudentAnswersForExam(int studentId, int examId);
        public IEnumerable<StudentAnswerResponseModel> GetSelectedOptionTextsForExam(int examId, int studentId);
        // IEnumerable<ExamResult> GetRecentExamResults(int studentId);
        ExamQuestion GetExamQuestionById(int id);
        void UpdateExamQuestion(ExamQuestion examQuestion);
        void DeleteExamQuestion(int id);
        public IEnumerable<ExamQuestion> GetExamQuestionsByTeacherId(int teacherId);
        public bool ExamExistsForClassAndTimeframe(int classId, DateTime startTime, DateTime endTime);


    }
}
