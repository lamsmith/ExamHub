using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Implementations;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using System.Security.Claims;

namespace ExamHub.Services.Implementations
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IHttpContextAccessor _httpContext;

        public ExamService(IExamRepository examRepository, ITeacherRepository teacherRepository, IHttpContextAccessor httpContext)
        {
            _examRepository = examRepository;
            _teacherRepository = teacherRepository;
            _httpContext = httpContext;
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

        public IEnumerable<ExamResponseModel> GetExamsForStudent(int studentId)
        {
            var getExamsForStudent = _examRepository.GetExamsForStudent(studentId);
            return getExamsForStudent.Select(e => new ExamResponseModel
            {              
                ExamName = e.ExamName,
                ExamQuestions = e.ExamQuestions,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                ClassId = e.ClassId,
                SubjectId = e.SubjectId,
            }); 
        }

        public IEnumerable<ExamResponseModel> GetUpcomingExamsByStudent(int studentId)
        {
            var upcomingExam = _examRepository.GetUpcomingExamsByStudent(studentId);
            return upcomingExam.Select(e => new ExamResponseModel
            {
                 ExamName = e.ExamName,
                 StartTime = e.StartTime,
                 EndTime = e.EndTime,

            });       
        }

        IEnumerable<ExamQuestionReponseModel> IExamService.GetQuestionsByExamId(int examId)
        {
            var getQuestionsByExamId = _examRepository.GetQuestionsByExamId(examId);
            return getQuestionsByExamId.Select(e => new ExamQuestionReponseModel
            {
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
                DateTime = DateTime.Now,
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                ClassId = exam.ClassId,
                CreatedBy = loginUserId.ToString()
            };
                  _examRepository.AddExam(exams);

            return exams.Id;
        }

        //public IEnumerable<ExamResponseModel> GetExamsByTeacher(string teacherName)
        //{
        //    throw new NotImplementedException();
        //}
        public IEnumerable<StudentExam> GetExamScoresByExamId(int examId)
        {
            return _examRepository.GetExamScoresByExamId(examId);
        }

        //public IEnumerable<ExamQuestion> GetQuestionsByExamId(int examId)
        //{
        //    return _examRepository.GetQuestionsByExamId(examId);
        //}

        public void SaveStudentAnswer(StudentAnswer studentAnswer)
        {
             _examRepository.SaveStudentAnswer(studentAnswer);
        }

        public void SaveStudentExam(StudentExam studentExam)
        {
             _examRepository.SaveStudentExam(studentExam);
        }

      
    }

}

