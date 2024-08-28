using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.ViewModel;

namespace ExamHub.Services.Inteface
{
    public interface IExamService
    {
        void AddExam(Exam exam);
        IEnumerable<ExamResponseModel> GetAllExams();
        IEnumerable<ExamResponseModel> GetExamsByTeacherId(int teacherId);
        IEnumerable<StudentExam> GetExamScoresByExamId(int examId);
        void SaveStudentAnswer(StudentAnswer studentAnswer);
        void SaveStudentExam(StudentExam studentExam);
        IEnumerable<ExamResponseModel> GetExamsForStudent(int classId,  int studentId);
        IEnumerable<ExamResponseModel> GetUpcomingExamsByClass(int classId);
        bool HasStudentTakenExam(int studentId, int examId);
        Exam GetExamById(int id);
        void UpdateExam(Exam exam);
        void DeleteExam(int id);
        int CreateExam(ExamRequestModel exam);
        public double CalculateScore(int studentId, int examId);
        public void SaveExamResult(ExamResult examResult);
        List<ExamQuestion> GetExamQuestionsForExam(int examId);

        IEnumerable<ExamQuestionReponseModel> GetQuestionsByExamId(int examId);
        ExamQuestion GetExamQuestionById(int id);
        void UpdateExamQuestion(ExamQuestion examQuestion);
        void DeleteExamQuestion(int id);
        public IEnumerable <ExamQuestion> GetExamQuestionsByTeacherId(int teacherId);
        public bool ExamExistsForClassAndTimeframe(int classId, DateTime startTime, DateTime endTime);



        public StudentResponseModel SaveExam(TakeExamViewModel takeExamViewModel);

       
      










    }
}
