using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IGeneralExamResultRepository
    {
        //Task<GeneralExamResult> GetGeneralExamResultByIdAsync(int id);
        //Task<GeneralExamResult> GetGeneralExamResultByStudentIdAsync(int studentId);
        //Task AddGeneralExamResultAsync(GeneralExamResult generalExamResult);
        //Task SaveAsync();

        public void SaveGeneralExamResult(int studentId, double percentage);
        Task<IEnumerable<GeneralExamResult>> GetResultsForPrincipalAsync(string principalName);
        Task<IEnumerable<GeneralExamResult>> GetResultsForTeacherAsync(string teacherName, int examId);
        Task<IEnumerable<GeneralExamResult>> GetResultsForStudentAsync(string studentName);
        GeneralExamResult GetGeneralExamResultByStudentId(int studentId);
    }
}
