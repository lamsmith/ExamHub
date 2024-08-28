using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Security.Claims;

namespace ExamHub.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IClassService _classService;
        private readonly IGeneralExamResultService _generalExamResultService;

        public TeacherController(ITeacherService teacherService, IGeneralExamResultService generalExamResultService, IStudentService studentService, IExamService examService, IClassService classService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _examService = examService;
            _classService = classService;
            _generalExamResultService = generalExamResultService;
        }

        public IActionResult Index()
        {
            var teacherName = User.Identity.Name;
            var teacher = _teacherService.GetTeacherByName(teacherName);
            if (teacher == null)
                return NotFound();
            var classTeachers = _teacherService.GetAllClassTeachers().Where(ct => ct.TeacherId == teacher.Id).ToList();
            var subjectTeachers = _teacherService.GetAllSubjectTeachers().Where(st => st.TeacherId == teacher.Id).ToList();

            var viewModel = new TeacherDashboardViewModel
            {
                TeacherId = teacher.Id,
                TeacherName = teacherName,
                Classes = classTeachers.Select(ct => new ClassResponseModel
                {
                    Id = ct.Class.Id,
                    ClassName = ct.Class.ClassName
                }).ToList(),
                CurrentClassId = classTeachers[0].Id,
                Subjects = subjectTeachers.Select(st => new SubjectResponseModel
                {
                    Id = st.Subject.Id,
                    SubjectName = st.Subject.SubjectName
                }).ToList()
            };
            return View(viewModel);
        }

        public IActionResult ViewStudents(int classId)
        {
            var students = _studentService.GetStudentsByClass(classId);


            var viewModel = new ClassStudentsViewModel
            {
                Students = students.Select(s => new StudentViewModel
                {
                    FullName = s.FirstName + " " + s.LastName,
                    ClassName = s.ClassName
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult SetExam(SetExamViewModel model)
        {
            var teacherName = User.Identity.Name;
            var teacher = _teacherService.GetTeacherByName(teacherName);

           
            bool examExists = _examService.ExamExistsForClassAndTimeframe(model.ClassId, model.StartTime, model.EndTime);

            if (examExists)
            {
               
                ModelState.AddModelError("", "An exam has already been set for this class and timeframe.");
                model.TeacherId = teacher.Id;
                model.Classes = teacher.ClassTeachers.Select(ct => ct.Class).ToList();
                model.Subjects = teacher.SubjectTeachers.Select(st => st.Subject).ToList();
                return View(model);
            }


            var viewModel = new SetExamViewModel
            {
                TeacherId = teacher.Id,
                Classes = teacher.ClassTeachers.Select(ct => ct.Class).ToList(),
                Subjects = teacher.SubjectTeachers.Select(st => st.Subject).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SetExam(ExamRequestModel model)
        {
             var exam = new Exam
                {
                 ExamName = model.ExamName,
                 CreatedByTeacherId = model.CreatedByTeacherId,
                 ClassId = model.ClassId,
                 SubjectId = model.SubjectId,
                 CreatedAt = DateTime.Now,
                 StartTime = model.StartTime,
                 EndTime = model.EndTime,
                 CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)
             };

               var examId = _examService.CreateExam(model);

                return RedirectToAction("UploadExamQuestions", "ExamQuestions", new {
                examId = examId
            });
        }

       


        public IActionResult ViewExamScores()
        {
            var teacherName = User.Identity.Name;
            var teacher = _teacherService.GetTeacherByName(teacherName);

            if (teacher == null)
                return NotFound();

            var exams = _examService.GetExamsByTeacherId(teacher.Id);

            var viewModel = new ViewExamScoresViewModel
            {
                Exams = exams.Select(e => new ExamResponseModel
                {
                    Id = e.Id,
                    ExamName = e.ExamName,
                    StartTime = e.StartTime,
                    EndTime = e.EndTime,
                    Class = e.Class,
                    Subject = e.Subject
                   
                    
                }).ToList()
            };

            return View(viewModel);
        }
        public IActionResult ViewExamScores(int examId)
        {
            var exam = _examService.GetExamById(examId);
            var studentExamScores = _examService.GetExamScoresByExamId(examId);

            var model = new ExamScoresViewModel
            {
                ExamId = examId,
                ExamName = exam.ExamName,
                StudentExamScores = studentExamScores.Select(s => new StudentExamScore
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.User.FirstName + " " + s.Student.User.LastName, 
                    Score = s.Score
                }).ToList()
            };
            return View(model);
        }

        public IActionResult ViewExamQuestions()
        {
            var stringUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(stringUserId);

            var teacher = _teacherService.GetTeacherByUserId(userId);

            var examQuestions = _examService.GetExamQuestionsByTeacherId(teacher.Id);

            if (examQuestions == null || !examQuestions.Any())
            {
                return NotFound();
            }

            var viewModel = examQuestions.Select(eq => new ExamQuestionsViewModel
            {
                Id = eq.Id,
                ExamId = eq.ExamId,
                QuestionNo = eq.QuestionNo,
                QuestionText = eq.QuestionText,
                CorrectAnswer = eq.CorrectAnswer,
                Options = eq.Options.Select(o => new OptionViewModel
                {
                    OptionId = o.Id,
                    OptionText = o.OptionText
                }).ToList()
            }).ToList();

            return View(viewModel);
        }




        [HttpPost]
        public IActionResult EditExamQuestion(EditExamQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new ExamQuestion
                {
                    Id = model.Id,
                    QuestionText = model.QuestionText,
                    Options = model.Options.Select(o => new Option
                    {
                        Id = o.OptionId,
                        OptionText = o.OptionText
                    }).ToList()
                };

                _examService.UpdateExamQuestion(question);

                return RedirectToAction("ViewExamQuestions", new { examId = model.Id });
            }

            return View(model);
        }

        public IActionResult DeleteExamQuestion(int id)
        {
            var question = _examService.GetExamQuestionById(id);
            if (question == null)
                return NotFound();

            var viewModel = new DeleteExamQuestionViewModel
            {
                Id = question.Id,
                QuestionText = question.QuestionText
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("DeleteExamQuestion")]
        public IActionResult DeleteExamQuestionConfirmed(int id)
        {
            _examService.DeleteExamQuestion(id);
            return RedirectToAction("ViewExamQuestions", new { examId = id });
        }
        public IActionResult ViewResults(int examId)
        {
            var stringUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = int.Parse(stringUserId);

            var teacher = _teacherService.GetTeacherByUserId(userId);


            var results = _generalExamResultService.GetResultsForTeacherAsync(teacher.User.Username, examId);
            return View(results);
        }
    }
}

