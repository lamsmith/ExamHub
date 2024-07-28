using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
   
        public class ExamQuestionService : IExamQuestionService
        {
            private readonly IExamQuestionRepository _repository;

            public ExamQuestionService(IExamQuestionRepository repository)
            {
                _repository = repository;
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
                var getExamQuestionId = _repository.GetExamQuestionById(id);
                return  new ExamQuestionReponseModel
                {
                     QuestionText   = getExamQuestionId.QuestionText,
                      Options   = getExamQuestionId.Options.Select(o =>new OptionResponseModel
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



        public CreateExamQuestionResponseModel CreateExamQuestion(CreateExamQuestionRequestModel request)
        {
            var questionId = new Random().Next(1, 1000);

            var options = request.Options.Select((optionText, index) => new Option
            {
                OptionText = optionText,
                OptionLabel = ((char)('A' + index)).ToString() 
            }).ToList();

            var examQuestion = new ExamQuestion
            {
                QuestionNo = questionId,
                QuestionText = request.QuestionText,
                Options = options,
                CorrectAnswer = request.CorrectAnswer,
                ExamId = request.ExamId
            };

            _repository.AddExamQuestion(examQuestion);

            return new CreateExamQuestionResponseModel
            {
                QuestionId = questionId,
                QuestionText = request.QuestionText,
                Options = request.Options,
                CorrectAnswer = request.CorrectAnswer
            };
        }
    }
    }
    

