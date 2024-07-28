using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IExamQuestionRepository
    {
        //Task<UploadedQuestion> AddExamQuestion(CreateExamQuestionRequestModel request);
        IEnumerable<ExamQuestion> GetAllExamQuestions();
        ExamQuestion GetExamQuestionById(int id);
        void AddExamQuestion(ExamQuestion examQuestion);
        void UpdateExamQuestion(ExamQuestion examQuestion);
        void DeleteExamQuestion(int id);


    }
}
