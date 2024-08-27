using ExamHub.Context;
using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Services.Implementations
{
   
        public class ExamQuestionService : IExamQuestionService
        {
            private readonly IExamQuestionRepository _repository;
            private readonly ApplicationDbContext _context;

            public ExamQuestionService(IExamQuestionRepository repository, ApplicationDbContext context)
            {
                _repository = repository;
                _context = context;
            }

            public IEnumerable<ExamQuestionReponseModel> GetAllExamQuestions()
            {
                var getAllExamQuestion  = _repository.GetAllExamQuestions();
                return getAllExamQuestion.Select(e => new ExamQuestionReponseModel
                {
                    QuestionText = e.QuestionText,
                    Options = e.Options.Select(o => new OptionResponseModel
                    {
                        OptionText = o.OptionText,
                    }).ToList(),
                });
            }
            public ExamQuestionReponseModel GetExamQuestionById(int id)
            {
                var getExamQuestionId = _context.ExamQuestions
                    .Include(eq => eq.Options)
                    .FirstOrDefault(eq => eq.Id == id);
                if (getExamQuestionId == null)
                {
                    throw new KeyNotFoundException("Exam question not found.");
                }
                return new ExamQuestionReponseModel
                {
                    QuestionText = getExamQuestionId.QuestionText,
                    Options = getExamQuestionId.Options?.Select(o => new OptionResponseModel
                    {
                        OptionText = o.OptionText,
                    }).ToList(),
                };
            }

        public void AddExamQuestion(ExamQuestion examQuestion)
            {
                _repository.AddExamQuestion(examQuestion);
            }

            public void UpdateExamQuestion(ExamQuestion examQuestion)
            {
                _repository.UpdateExamQuestion(examQuestion);
            }

            public void DeleteExamQuestion(int id)
            {
                _repository.DeleteExamQuestion(id);
            }

            public IEnumerable<int> GetCorrectAnswersForExam(int examId)
            {
            
                var correctAnswers = _context.ExamQuestions
                    .Where(q => q.ExamId == examId)
                    .Select(q => q.CorrectAnswer)
                    .ToList();


                return correctAnswers;
            }





        public CreateExamQuestionResponseModel CreateExamQuestion(CreateExamQuestionRequestModel request)
        {
            // Create options and assign labels (A, B, C, etc.)
            var options = request.Options.Select((optionText, index) => new Option
            {
                OptionText = optionText,
                OptionLabel = ((char)('A' + index)).ToString()
            }).ToList();


            // Create the exam question with the correct option ID
            var examQuestion = new ExamQuestion
            {
                QuestionNo = request.QuestionNo,
                QuestionText = request.QuestionText,
                Options = options,
                ExamId = request.ExamId
            };

          
            _repository.AddExamQuestion(examQuestion);


        
            var correctOption = examQuestion.Options.FirstOrDefault(o => o.OptionLabel == request.CorrectAnswer);

            examQuestion.CorrectAnswer = correctOption.Id;

            _repository.UpdateExamQuestion(examQuestion);

            
            return new CreateExamQuestionResponseModel
            {
                QuestionId = examQuestion.QuestionNo,
                QuestionText = examQuestion.QuestionText,
                Options = request.Options,
                CorrectAnswer = request.CorrectAnswer 
            };
        }

    
    }
}
    

