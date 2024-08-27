using ExamHub.Entity;
using ExamHub.ViewModel;

namespace ExamHub.Services.Inteface
{
    public interface IGeneralExamResultService
    {
        //    Task<GeneralExamResult> GetGeneralExamResultByStudentIdAsync(int studentId);
        //}
        Task<IEnumerable<GeneralExamResult>> GetAllResultsForPrincipalAsync(string principalName);
        Task<IEnumerable<GeneralExamResult>> GetResultsForTeacherAsync(string teacherName, int examId);
        Task<IEnumerable<GeneralExamResult>> GetResultsForStudentAsync(string studentName);
        public GeneralExamResultViewModel GetGeneralExamResultForStudent(int studentId);
    }
}
