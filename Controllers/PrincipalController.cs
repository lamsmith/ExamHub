
using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Services.Implementations;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamHub.Controllers
{
    public class PrincipalController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        public PrincipalController(ITeacherService teacherService, IStudentService studentService, IClassService classService, ISubjectService  subjectService)
        {
            _teacherService = teacherService;
            _studentService = studentService;
            _classService = classService;
            _subjectService = subjectService;
        }
        public IActionResult Index()
        {
            var phgvu = new PrincipalDashboardViewModel
            {
                PrincipalName = User.Identity.Name,
                TotalTeachers = _teacherService.GetTotalTeachers(),
                TotalStudents = _studentService.GetTotalStudents(),
                Classes = _classService.GetAllClasses()
            };
            return View(phgvu);
        }


        public IActionResult ViewStudents(int classId)
        {
            var students = _studentService.GetStudentsByClass(classId);
            var classes = _classService.GetAllClasses();

            var model = new PrincipalDashboardViewModel
            {
                Students = students,
                Classes = classes
            };

            return View(model);
        }

        public IActionResult ViewTeachers()
        {
            var teachers = _teacherService.GetAllTeachersByPrincipal ();
            var classes = _classService.GetAllClasses();
            var classTeachers = _teacherService.GetAllClassTeachers();
            var subjectTeachers = _teacherService.GetAllSubjectTeachers();

            var model = new TeacherViewModel
            {
                Teachers = teachers,
                Classes = classes,
                ClassTeachers = classTeachers,
                SubjectTeachers = subjectTeachers,
            };

            return View(model);
        }

        public IActionResult Settings()
        {
            return View();
        }

       [HttpGet]
        public IActionResult CreateTeacher()
        {
            var viewModel = new CreateTeacherViewModel
            {
                Classes = _classService.GetAllClasses(),
                Subjects = _subjectService.GetAllSubjects()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateTeacher(CreateTeacherRequestModel model)
        {
                _teacherService.CreateTeacher(model);
                return RedirectToAction("ViewTeachers");
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            var clas = _classService.GetAllClasses();
            ViewBag.Class = new SelectList(clas, "Id", "ClassName");
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentRequestModel model)
        {
            if (ModelState.IsValid)
            {
                _studentService.CreateStudent(model);
                return RedirectToAction("Index");
            }
            // Repopulate classes in case of validation errors
            var classes = _classService.GetAllClasses();
            ViewBag.Class = new SelectList(classes, "Id", "ClassName");

            return View(model);
        }

        public IActionResult CreateSubject()
        {
            return View(new CreateSubjectViewModel());
        }

        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                _subjectService.CreateSubject(model.Name);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult AssignSubject()
        {
            var classes = _classService.GetAllClasses();
            var subjects = _subjectService.GetAllSubjects();

            var model = new AssignSubjectViewModel
            {
                Classes = classes,
                Subjects = subjects
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignSubject(AssignSubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                _classService.AssignSubjectToClass(model.ClassId, model.SubjectId);
                return RedirectToAction("Index");
            }

            var classes = _classService.GetAllClasses();
            var subjects = _subjectService.GetAllSubjects();
            model.Classes = classes;
            model.Subjects = subjects;

            return View(model);
        }
    

    public IActionResult AssignTeacher()
        {
            var teachers = _teacherService.GetAllTeachersByPrincipal();
            var classes = _classService.GetAllClasses();
            var subjects = _subjectService.GetAllSubjects();

            var model = new AssignTeacherViewModel
            {
                Teachers = teachers,
                Classes = classes,
                Subjects = subjects
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignTeacher(AssignTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                _teacherService.AssignTeacherToClassAndSubject(model.TeacherId, model.ClassId, model.SubjectId);
                return RedirectToAction("Index");
            }

            var teachers = _teacherService.GetAllTeachersByPrincipal();
            var classes = _classService.GetAllClasses();
            var subjects = _subjectService.GetAllSubjects();
            model.Teachers = teachers;
            model.Classes = classes;
            model.Subjects = subjects;

            return View(model);
        }

        public IActionResult AssignStudent()
        {
            var students = _studentService.GetAllStudent();
            var classes = _classService.GetAllClasses();

            var model = new AssignStudentViewModel
            {
                Students = students,
                Classes = classes
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignStudent(AssignStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                _studentService.AssignStudentToClass(model.StudentId, model.ClassId);
                return RedirectToAction("Index");
            }

            var students = _studentService.GetAllStudent();
            var classes = _classService.GetAllClasses();
            model.Students = students;
            model.Classes = classes;

            return View(model);
        }
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View("Settings");
        }

        public IActionResult Logout()
        {
            
            return RedirectToAction("Index", "Home");
        }

    }

}

