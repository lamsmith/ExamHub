using System.Security.Claims;
using ExamHub.Entity;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ExamHub.DTO;
using NuGet.DependencyResolver;


namespace ExamHub.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly INotificationService _notificationService;
        private readonly IClassService _classService;

        public StudentController(IStudentService studentService, IExamService examService, INotificationService notificationService)
        {
            _studentService = studentService;
            _examService = examService;
             _notificationService = notificationService;
        }

        public IActionResult Index()
        {
            var stringuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = int.Parse(stringuserId);
            var student = _studentService.GetStudentByUserId(userId);
            //  var classStudents = _studentService.GetStudentsByClass(student.).Where(x => x.StudentId == studentId).ToList();

            var dashboardViewModel = new StudentDashboardViewModel
            {
                CurrentClassId = student.ClassStudents.OrderByDescending(x => x.DateJoin).First().ClassId,
                StudentId = student.Id,
                Name = student.User.FirstName + student.User.LastName,
                Classes = student.ClassStudents.Select(x => new ClassResponseModel
                {
                    Id = x.Id,
                    ClassName = x.Class.ClassName,
                    Exams = x.Class.Exams.Select(x => new ExamResponseModel
                    {
                        
                        CreatedAt = DateTime.Now,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        Subject = x.Subject.SubjectName
                        
                    }).ToList()

                }
                 ).ToList(),
              //  UpcomingExams = _examService.GetUpcomingExamsByStudent(student.Id),
              //  RecentResults = _studentService.GetRecentExamResults(studentId),
              //  Notifications = _notificationService.GetNotificationsForStudent(studentId)
            };

            // Aggregate upcoming exams for all classes the student is enrolled in
            var upcomingExams = new List<ExamResponseModel>();
            foreach (var classStudent in student.ClassStudents)
            {
                var classUpcomingExams = _examService.GetUpcomingExamsByClass(classStudent.ClassId)
                    .Select(e => new ExamResponseModel
                    {
                        ExamName = e.ExamName,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Subject = e.Subject
                    });

                upcomingExams.AddRange(classUpcomingExams);
            }

            // Assign the aggregated upcoming exams to the dashboard view model
            dashboardViewModel.UpcomingExams = upcomingExams;




            return View(dashboardViewModel);


        }



        public IActionResult TakeExam(int examId)
        {
            var exam = _examService.GetExamById(examId);

            if (exam == null)
            {
                return NotFound();
            }

            var now = DateTime.Now;
            if (now < exam.StartTime)
            {
                TempData["ErrorMessage"] = "You cannot start the exam until the assigned start time.";
                return RedirectToAction("Exams", new { classId = exam.ClassId });
            }

            var questions = _examService.GetQuestionsByExamId(examId)
                .Select(q => new QuestionViewModel
                {
                    QuestionId = q.Id,
                    QuestionText = q.QuestionText,
                    Options = q.Options.Select(o => new OptionViewModel
                    {
                        OptionId = o.Id,
                        OptionText = o.OptionText
                    }).ToList()
                }).ToList();

            var model = new TakeExamViewModel
            {
                ExamId = exam.Id,
                ExamName = exam.ExamName,
                Questions = questions
            };

            return View(model);
        }



        public IActionResult SubmitExam(TakeExamViewModel model)
        {
            var stringuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(stringuserId);
            var student = _studentService.GetStudentByUserId(userId);

            foreach (var question in model.Questions)
            {
                var studentAnswer = new StudentAnswer
                {
                    StudentId = student.Id,
                    QuestionId = question.QuestionId,
                    SelectedOptionId = question.SelectedOptionId
                };
                _examService.SaveStudentAnswer(studentAnswer);
            }

            var exam = _examService.GetExamById(model.ExamId);
            if (exam == null)
            {
                return NotFound("Exam not found.");
            }

            var studentExam = new StudentExam
            {
                StudentId = student.Id,
                ExamId = model.ExamId,
                Completed = true,
                CompletionTime = DateTime.Now
            };
            _examService.SaveStudentExam(studentExam);

            return RedirectToAction("Dashboard");
        }


        public IActionResult Exams(int classId)
        {
            // Get exams for the class
            var examResponseModels = _examService.GetExamsForStudent(classId);

            var exams = examResponseModels.Select(e => new ExamViewModel
            {
                ExamId = e.Id,
                Class = e.Class,
                Subject = e.Subject,
                StartTime = e.StartTime,
                EndTime = e.EndTime
            }).ToList();

            var examsViewModel = new ExamsViewModel
            {
                Exams = exams
            };

            return View(examsViewModel);
        }






    }
}


