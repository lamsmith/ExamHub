using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface IExamResultService
    {
        Task<GeneralExamResult> CalculateAndSaveGeneralExamResultAsync(int studentId, int examId);
    }
}
