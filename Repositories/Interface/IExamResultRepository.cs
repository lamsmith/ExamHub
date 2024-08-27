using ExamHub.Entity;
using System.Threading.Tasks;

namespace ExamHub.Repositories.Interface
{
    public interface IExamResultRepository
    {
        Task<ExamResult> GetExamResultByIdAsync(int id);
        Task<IEnumerable<ExamResult>> GetExamResultsByStudentIdAsync(int studentId);
        Task<IEnumerable<ExamResult>> GetExamResultsByExamIdAsync(int examId);
        Task AddExamResultAsync(ExamResult examResult);
        bool HasStudentTakenExam(int studentId, int examId);
        Task SaveAsync();
    }
}
