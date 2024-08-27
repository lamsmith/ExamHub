using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface IExamQuestionService
    {
        IEnumerable<ExamQuestionReponseModel> GetAllExamQuestions();
        ExamQuestionReponseModel GetExamQuestionById(int id);
        void AddExamQuestion(ExamQuestion examQuestion);
        void UpdateExamQuestion(ExamQuestion examQuestion);
        void DeleteExamQuestion(int id);
        public IEnumerable<int> GetCorrectAnswersForExam(int examId);
        CreateExamQuestionResponseModel CreateExamQuestion(CreateExamQuestionRequestModel request);
        

    }
}
