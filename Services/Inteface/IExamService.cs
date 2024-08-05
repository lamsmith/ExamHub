using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface IExamService
    {
        void AddExam(Exam exam);
        IEnumerable<ExamResponseModel> GetAllExams();
        IEnumerable<ExamResponseModel> GetExamsByTeacherId(int teacherId);
        //IEnumerable<ExamResponseModel> GetExamsByTeacher(string teacherName);
        IEnumerable<StudentExam> GetExamScoresByExamId(int examId);
        IEnumerable<ExamQuestionReponseModel> GetQuestionsByExamId(int examId);
        void SaveStudentAnswer(StudentAnswer studentAnswer);
        void SaveStudentExam(StudentExam studentExam);
        IEnumerable<ExamResponseModel> GetExamsForStudent(int classId);
        IEnumerable<ExamResponseModel> GetUpcomingExamsByClass(int classId);
        Exam GetExamById(int id);
        void UpdateExam(Exam exam);
        void DeleteExam(int id);
        int CreateExam(ExamRequestModel exam);

       







    }
}
