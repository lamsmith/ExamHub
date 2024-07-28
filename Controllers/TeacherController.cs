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

        public TeacherController(ITeacherService teacherService, IStudentService studentService, IExamService examService, IClassService classService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _examService = examService;
            _classService = classService;
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
                Subjects = subjectTeachers.Select(st => new SubjectResponseModel
                {
                    Id = st.Subject.Id,
                    SubjectName = st.Subject.SubjectName
                }).ToList()
            };
            return View(viewModel);
        }

        public IActionResult SetExam()
        {
            var teacherName = User.Identity.Name;
            var teacher = _teacherService.GetTeacherByName(teacherName);

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
                 DateTime = DateTime.Now, 
                 StartTime = model.StartTime,
                 EndTime = model.EndTime,
                 CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)
             };

               var examId = _examService.CreateExam(model);

                return RedirectToAction("UploadExamQuestions", "ExamQuestions", new {
                examId = examId
            });
            if (ModelState.IsValid)
            {
               
            }

            return View(model);
        }

        public IActionResult ViewStudents(int classId)
        {
             var students = _studentService. GetStudentsByClass(classId);

            var viewModel = new ClassStudentsViewModel
            {
                Students = students.Select(s => new StudentViewModel
                {
                    StudentId = s.Id,
                    StudentName = s.FristName + " " + s.LastName,
                    
                }).ToList()
            };


            return View(viewModel);
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
    }
}

