using System.Security.Claims;
using ExamHub.Entity;
using Microsoft.AspNetCore.Http;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ExamHub.DTO;
using NuGet.DependencyResolver;
using ExamHub.Repositories.Interface;
using ExamHub.Repositories.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ExamHub.Context;


namespace ExamHub.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly INotificationService _notificationService;
        private readonly IClassService _classService;
        private readonly IGeneralExamResultService _generalExamResultService;
        private readonly IStudentRepository _studentRepository;
        private readonly ApplicationDbContext _context;

        public StudentController(IStudentService studentService, IExamService examService, INotificationService notificationService, IGeneralExamResultService generalExamResultService, ApplicationDbContext context)
        {
            _studentService = studentService;
            _examService = examService;
             _notificationService = notificationService;
            _generalExamResultService = generalExamResultService;
            _context = context;


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
            var stringUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(stringUserId);
            var student = _studentService.GetStudentByUserId(userId);

            if (student == null)
            {
                return NotFound();
            }

            var exam = _examService.GetExamById(examId);
            if (exam == null)
            {
                return NotFound();
            }

            // Check if the student has already taken the exam
            var studentExam = _context.StudentExams
                .FirstOrDefault(se => se.ExamId == examId && se.StudentId == student.Id);

            if (studentExam != null && studentExam.Completed)
            {
                TempData["ErrorMessage"] = "You have already taken this exam.";
                return RedirectToAction("Exams", new { classId = exam.ClassId });
            }

            var now = DateTime.Now;
            // Check if the current time is before the exam start time
            if (now < exam.StartTime)
            {
                TempData["ErrorMessage"] = "You cannot start the exam until the assigned start time.";
                return RedirectToAction("Exams", new { classId = exam.ClassId });
            }

            // Check if the current time is after the exam end time
            if (now > exam.EndTime)
            {
                TempData["ErrorMessage"] = "The time to take this exam has expired.";
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
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                Questions = questions
            };

            return View(model);
        }






        public IActionResult SubmitExam(TakeExamViewModel model)
            {
            if (model.ExamId == 0)
            {
                
                ModelState.AddModelError("", "Exam ID is not valid.");
                return View(model);
            }

            var saveExam = _examService.SaveExam(model);
           
            var studentExam = new StudentExam
            {
                StudentId = saveExam.Id,
                ExamId = model.ExamId,
                Completed = true,
                CompletionTime = DateTime.Now
            };
            _examService.SaveStudentExam(studentExam);

           
            double percentage = _examService.CalculateScore(saveExam.Id, model.ExamId);

            var generalExamResult = _context.GeneralExamResults
                .FirstOrDefault(g => g.StudentId == studentExam.Id);

            if (generalExamResult == null)
            {
        
                generalExamResult = new GeneralExamResult
                {
                    StudentId = studentExam.Id,
                    Percentage = percentage
                };
                _context.GeneralExamResults.Add(generalExamResult);
                _context.SaveChanges(); 
            }

     
            var examResult = new ExamResult
            {
                GeneralExamResultId = generalExamResult.Id, 
                StudentId = studentExam.Id,
                ExamId = model.ExamId,
                Score = (int)(percentage / 100 * model.Questions.Count),
                Percentage = percentage,
                ExamDate = DateTime.Now
            };

            _examService.SaveExamResult(examResult);


            return RedirectToAction("Index");
        }




  

        public IActionResult Exams(int classId)
        {
            // Get exams for the class
            var stringuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var studentId = int.Parse(stringuserId);
  
            var examResponseModels = _examService.GetExamsForStudent(classId, studentId);

            var exams = examResponseModels.Select(e => new ExamViewModel
            {
                ExamId = e.Id,
                Class = e.Class,
                Subject = e.Subject,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                HasTaken = _examService.HasStudentTakenExam(studentId, e.Id)
            }).ToList();

            var examsViewModel = new ExamsViewModel
            {
                Exams = exams
            };

            return View(examsViewModel);
        }


        public IActionResult ViewResults()
        {
            var stringUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(stringUserId);

     
            var student = _studentService.GetStudentByUserId(userId);

            if (student == null)
            {
                return NotFound("Student not found");
            }
       
            var generalExamResult = _generalExamResultService.GetGeneralExamResultForStudent(student.Id);

            if (generalExamResult == null)
            {
                return NotFound("Results not found");
            }

            return View(generalExamResult);
        }







    }
}


