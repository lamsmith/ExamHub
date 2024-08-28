
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
                TotalClasses = _classService.GetTotalClasses(),
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
            return View(new BaseResponse());
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _studentService.CreateStudent(model);
                var clas = _classService.GetAllClasses();
                ViewBag.Class = new SelectList(clas, "Id", "ClassName");
                return View(response);
            }
            var @class = _classService.GetAllClasses();
            ViewBag.Class = new SelectList(@class, "Id", "ClassName");
            return View(new BaseResponse { Message="Model not valid", Status = false});
            
        }

      

        //[HttpPost]
        //public IActionResult CreateSubject(CreateSubjectViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _subjectService.CreateSubject(model.Name);
        //        return RedirectToAction("Index");
        //    }

        //    return View(model);
        //}

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


        [HttpGet]
        public IActionResult ManageClasses()
        {
            var classes = _classService.GetAllClasses();
            return View(classes);
        }

        [HttpGet]
        public IActionResult ManageSubjects()
        {
            var subjects = _subjectService.GetAllSubjects();
            return View(subjects);
        }

        [HttpGet]
        public IActionResult CreateClass()
        {
            return View(new CreateClassRequestModel());
        }

        [HttpPost]
        public IActionResult CreateClass(CreateClassRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var stringuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var principalId = int.Parse(stringuserId);
              
                var newClass = new Class
                {
                    ClassName = model.ClassName,
                    CreatedByPrincipalId = principalId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = stringuserId

                };
                _classService.CreateClass(newClass);
                return RedirectToAction("ManageClasses");

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditClass(int id)
        {
            var classEntity = _classService.GetClassById(id);
            return View(classEntity);
        }

        [HttpPost]
        public IActionResult EditClass(Class updatedClass)
        {
            if (ModelState.IsValid)
            {
                _classService.UpdateClass(updatedClass);
                return RedirectToAction("ManageClasses");
            }
            return View(updatedClass);
        }

        [HttpPost]
        public IActionResult DeleteClass(int id)
        {
            _classService.DeleteClass(id);
            return RedirectToAction("ManageClasses");
        }

        [HttpGet]
        public IActionResult CreateSubject()
        {
            return View(new CreateSubjectRequestModel());
        }


        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var stringuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var principalId = int.Parse(stringuserId);

                var subject = new Subject
                {
                    SubjectName = model.SubjectName,
                    CreatedBy = stringuserId,
                    PrincipalId = principalId,
                    CreatedAt = DateTime.Now,
                    
                };

                _subjectService.CreateSubject(subject);
                return RedirectToAction("ManageSubjects");
            }
            return View(model);
        }

            [HttpGet]
            public IActionResult EditSubject(int id)
            {
                var subject = _subjectService.GetAllSubjects().FirstOrDefault(s => s.Id == id);
                return View(subject);
            }

            [HttpPost]
            public IActionResult EditSubject(Subject subject)
            {
                if (ModelState.IsValid)
                {
                    _subjectService.CreateSubject(subject);
                    return RedirectToAction("ManageSubjects");
                }
                return View(subject);
            }


        [HttpPost]
        public IActionResult DeleteSubject(int id)
        {
            _subjectService.DeleteSubject(id);
            return RedirectToAction("ManageSubjects");
        }


        public IActionResult EditTeacher(int id)
        {
            var teacher = _teacherService.GetTeacherById(id);
            if (teacher == null)
            {
                return NotFound();
            }

            // Retrieve and map Class entities to ClassResponseModel DTOs
            var allClassDtos = _classService.GetAllClasses().Select(c => new ClassResponseModel
            {
                Id = c.Id,
                ClassName = c.ClassName
            }).ToList();

            // Retrieve and map Subject entities to SubjectResponseModel DTOs
            var allSubjectDtos = _subjectService.GetAllSubjects().Select(s => new SubjectResponseModel
            {
                Id = s.Id,
                SubjectName = s.SubjectName
            }).ToList();

            var model = new EditTeacherViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.User.FirstName,
                LastName = teacher.User.LastName,
                AssignedClassIds = teacher.ClassTeachers.Select(ct => ct.ClassId).ToList(),
                AssignedSubjectIds = teacher.SubjectTeachers.Select(st => st.SubjectId).ToList(),
                AllClasses = allClassDtos, // Assign mapped ClassResponseModel DTOs
                AllSubjects = allSubjectDtos // Assign mapped SubjectResponseModel DTOs
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult EditTeacher(EditTeacherViewModel model)
        {
                var teacher = _teacherService.GetTeacherById(model.Id);
                if (teacher == null)
                {
                    return NotFound();
                }

                teacher.User.FirstName = model.FirstName;
                teacher.User.LastName = model.LastName;
            foreach (var item in model.AssignedSubjectIds)
            {
                teacher.SubjectTeachers.Add(new SubjectTeacher
                {
                    SubjectId = item
                });
            }
            foreach (var item in model.AssignedClassIds)
            {
                teacher.ClassTeachers.Add(new ClassTeacher
                {
                    ClassId = item
                });
            }
            _teacherService.UpdateTeacher(teacher);
                return RedirectToAction("ViewTeachers");
            
        }

        [HttpPost]
        public IActionResult DeleteTeacher(int id)
        {
            _teacherService.DeleteTeacher(id);
            return RedirectToAction("ViewTeachers");
        }

    }

}

