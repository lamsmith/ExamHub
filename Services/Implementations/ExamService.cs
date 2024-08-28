using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Implementations;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace ExamHub.Services.Implementations
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IExamResultRepository _examResultRepository;
        private readonly IExamQuestionRepository _examQuestionRepository;
        private readonly IStudentRepository _studentRepository; 
        private readonly IExamQuestionService _examQuestionService;
        private readonly IStudentService _studentService;
        private readonly IListAnswerReposity _listAnswerReposity;
        

        public ExamService(IExamRepository examRepository, ITeacherRepository teacherRepository, IHttpContextAccessor httpContext, IExamResultRepository examResultRepository, IExamQuestionService examQuestionService, IStudentService studentService, IStudentRepository studentRepository, IListAnswerReposity listAnswerReposity)
        {
            _examRepository = examRepository;
            _teacherRepository = teacherRepository;
            _httpContext = httpContext;
            _examResultRepository = examResultRepository;
            _examQuestionService = examQuestionService;
            _studentService = studentService;
            _studentRepository = studentRepository;
            _listAnswerReposity = listAnswerReposity;
        }

        public void AddExam(Exam exam)
        {
            _examRepository.AddExam(exam);
        }

        public IEnumerable<ExamResponseModel> GetAllExams()
        {
            var getAllExams = _examRepository.GetAllExams();
            return getAllExams.Select(e => new ExamResponseModel
            {
                ExamName = e.ExamName,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                ClassId = e.ClassId,
            });
        }

        public IEnumerable<ExamResponseModel> GetExamsByTeacherId(int teacherId)    
        {
            var getExambyTeacherId = _examRepository.GetExamsByTeacherId(teacherId);
            return getExambyTeacherId.Select(t => new ExamResponseModel
            {
                 ExamName = t.ExamName,
                 ExamQuestions = t.ExamQuestions,
                 StartTime = t.StartTime,
                 EndTime = t.EndTime,
                 ClassId = t.ClassId,
                 SubjectId = t.SubjectId,
                  
            });
        }
        public IEnumerable<ExamResponseModel> GetExamsForStudent(int classId, int studentId)
        {
            var exams = _examRepository.GetExamsForStudent(classId);
            return exams.Select(e => new ExamResponseModel
            {
                Id = e.Id,
                
                ExamName = e.ExamName,
                ExamQuestions = e.ExamQuestions,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                ClassId = e.ClassId,
                SubjectId = e.SubjectId,
                Subject = e.Subject.SubjectName,
                 HasTaken = _examResultRepository.HasStudentTakenExam(studentId, e.Id)

            });
        }

          public bool HasStudentTakenExam(int studentId, int examId)
        {
            return _examResultRepository.HasStudentTakenExam(studentId, examId);
        }


        public IEnumerable<ExamResponseModel> GetUpcomingExamsByClass(int classId)
        {
            var upcomingExam = _examRepository.GetUpcomingExamsByClass(classId);
            return upcomingExam.Select(e => new ExamResponseModel
            {
                 ExamName = e.ExamName,
                 StartTime = e.StartTime,
                 EndTime = e.EndTime,
                 Subject = e.Subject.SubjectName

            });       
        }

        IEnumerable<ExamQuestionReponseModel> IExamService.GetQuestionsByExamId(int examId)
        {
            var getQuestionsByExamId = _examRepository.GetQuestionsByExamId(examId);
            return getQuestionsByExamId.Select(e => new ExamQuestionReponseModel
            {
                Id =e.Id,
                QuestionText = e.QuestionText,
                
                Options = e.Options.Select(o => new OptionResponseModel
                {
                    Id = o.Id,
                    OptionText = o.OptionText,
                }).ToList()
            }).ToList();
        }

      

        public Exam GetExamById(int id)
        {
            return _examRepository.GetExamById(id);
        }

        public void UpdateExam(Exam exam)
        {
            _examRepository.UpdateExam(exam);
        }

        public void DeleteExam(int id)
        {
            _examRepository.DeleteExam(id);
        }
        public int CreateExam(ExamRequestModel exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            var loginUserId = int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var teacher = _teacherRepository.GetTeacherByUserId(loginUserId);
            // Map ExamRequestModel to Exam entity
            var exams = new Exam
            {
                ExamName = exam.ExamName,
                CreatedByTeacherId = teacher.Id,
                SubjectId = exam.SubjectId,
                CreatedAt = DateTime.Now,
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                ClassId = exam.ClassId,
                CreatedBy = loginUserId.ToString()
            };
                  _examRepository.AddExam(exams);

            return exams.Id;
        }

     
        public IEnumerable<StudentExam> GetExamScoresByExamId(int examId)
        {
            return _examRepository.GetExamScoresByExamId(examId);
        }

        public void SaveExamResult(ExamResult examResult)
        {
            _examRepository.SaveExamResult(examResult);
        }
        public List<ExamQuestion> GetExamQuestionsForExam(int examId)
        {
            return _examRepository.GetExamQuestionsForExam(examId);
        }

        public void SaveStudentAnswer(StudentAnswer studentAnswer)
        {
             _examRepository.SaveStudentAnswer(studentAnswer);
        }

        public void SaveStudentExam(StudentExam studentExam)
        {
             _examRepository.SaveStudentExam(studentExam);
        }
        public StudentResponseModel SaveExam(TakeExamViewModel takeExamViewModel)
        {
            var stringUserId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(stringUserId);
            var student = _studentRepository.GetStudentByUserId(userId);

            var exam = _examRepository.GetExamById(takeExamViewModel.ExamId);
            var listanswer = new ListAnswer
            {

                StudentId = student.Id,
                ExamId = exam.Id,
                CreatedAt = DateTime.Now,
                CreatedBy = stringUserId

            };
            _listAnswerReposity.Add(listanswer);

            // Save student's answers
            foreach (var question in takeExamViewModel.Questions)
            {
                var studentAnswer = new StudentAnswer
                {
                    StudentId = student.Id,
                    QuestionId = question.QuestionId,
                    SelectedOptionId = question.SelectedOptionId,
                    ListAnswerId = listanswer.Id
                };
                _examRepository.SaveStudentAnswer(studentAnswer);
            }

            return new StudentResponseModel
            {
                FirstName = student.User.FirstName,
                LastName = student.User.LastName,
                Id = student.Id
            };
        }

        public double CalculateScore(int studentId, int examId)
        {
            var correctAnswers = _examQuestionService.GetCorrectAnswersForExam(examId);

            if (correctAnswers == null || !correctAnswers.Any())
            {
                throw new Exception("No correct answers found for the specified exam.");
            }

            
            var studentAnswers = _studentRepository.GetStudentAnswersForExam(studentId, examId).ToList();

            if (studentAnswers == null || !studentAnswers.Any())
            {
                throw new Exception("No student answers found for the specified exam.");
            }

            int score = 0;
            int totalQuestions = correctAnswers.Count();

            
            for (int i = 0; i < studentAnswers.Count; i++)
            {
                var answer = studentAnswers[i];
                if (answer.SelectedOptionId == answer.ExamQuestion.CorrectAnswer)
                {
                    score++;
                }
            }

            // Calculate the percentage
            double percentage = ((double)score / totalQuestions) * 100;

            return percentage;
        }

        public ExamQuestion GetExamQuestionById(int id)
        {
            return _examRepository.GetExamQuestionById(id);
        }

        public void UpdateExamQuestion(ExamQuestion examQuestion)
        {
            _examRepository.UpdateExamQuestion(examQuestion);
        }

        public void DeleteExamQuestion(int id)
        {
            _examRepository.DeleteExamQuestion(id);
        }







    }

}

