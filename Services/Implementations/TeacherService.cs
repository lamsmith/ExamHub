using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Enum;
using ExamHub.Repositories.Implementations;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using ExamHub.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectTeacherRepository _subjectTeacherRepository;
        private readonly IClassTeacherRepository _classTeacherRepository;

        public TeacherService(ITeacherRepository teacherService, ISubjectTeacherRepository subjectTeacherRepository,IClassTeacherRepository classTeacherRepository)
        {
            _teacherRepository = teacherService ;
            _subjectTeacherRepository = subjectTeacherRepository;
            _classTeacherRepository = classTeacherRepository;
        }

        public IEnumerable<TeacherResponseModel> GetAllTeachersByPrincipal()
        {
            var teachers = _teacherRepository.GetAllTeachersByPrincipal();
            return teachers.Select(t => new TeacherResponseModel
            {
                Id = t.Id,
                FristName = t.User.FirstName,
                LastName = t.User.LastName,
                 Subjects = t.SubjectTeachers.Select(s => s.Subject).ToList(),
                 Class = t.ClassTeachers.Select(s => s.Class).ToList(),
            });
        }

        public IEnumerable<ExamResponseModel> GetExamsByTeacher(string teacherName)
        {
            var exams = _teacherRepository.GetExamsByTeacher(teacherName);
            return exams.Select(e => new ExamResponseModel
            {
                Id = e.Id,
                ExamName = e.ExamName,
                Subject = e.Subject.SubjectName,
                CreatedAt = DateTime.Now,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
             
            });
        }
        public int GetTotalTeachers()
        {
            return _teacherRepository.GetTotalTeachers();
        }
           public IEnumerable<ClassTeacher> GetAllClassTeachers()
        {
            return _teacherRepository.GetAllClassTeachers();
        }

        public IEnumerable<SubjectTeacher> GetAllSubjectTeachers()
        {
            return _teacherRepository.GetAllSubjectTeachers();
        }

        public void CreateTeacher(Teacher teacher)
        {
            _teacherRepository.CreateTeacher(teacher);
        }

        public Teacher GetTeacherByName(string teacherName)
        {
            return _teacherRepository.GetTeacherByName(teacherName);     
        }
        

        public void AssignTeacherToClassAndSubject(int teacherId, int classId, int subjectId)
        {
            _teacherRepository.AssignTeacherToClass(teacherId, classId);
            _teacherRepository.AssignTeacherToSubject(teacherId, subjectId);
        }

        public void CreateTeacher(CreateTeacherRequestModel model)
        {
            var user = new User
            {
                Username = model.UserName,
                Password = model.Password,
                CreatedAt = DateTime.Now,
                FirstName =model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                RoleId = 2,
                CreatedBy = "1",
            };

            var teacher = new Teacher
            {
                User = user,
                CreatedAt = DateTime.Now,
                CreatedBy = "1"
                
            };

            var subjectTeacher = new SubjectTeacher
            {
                Teacher = teacher,
                SubjectId = model.SubjectId
            };

           
            var classTeacher = new ClassTeacher
            {
                Teacher = teacher,
                ClassId = model.ClassId
            };

            _teacherRepository.CreateTeacher(teacher);
            _subjectTeacherRepository.CreateSubjectTeacher(subjectTeacher);
            _classTeacherRepository.CreateClassTeacher(classTeacher);
        }

        public Teacher GetTeacherByUserId(int userId)
        {
            return _teacherRepository.GetTeacherByUserId(userId);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            _teacherRepository.UpdateTeacher(teacher);
        }

        public void RemoveTeacherFromClass(int teacherId, int classId)
        {
            _teacherRepository.RemoveTeacherFromClass(teacherId, classId);
        }

        public void RemoveTeacherFromSubject(int teacherId, int subjectId)
        {
            _teacherRepository.RemoveTeacherFromSubject(teacherId, subjectId);
        }

        public void DeleteTeacher(int teacherId)
        {
            _teacherRepository.DeleteTeacher(teacherId);
        }

        public Teacher GetTeacherById(int id)
        {
            return _teacherRepository.GetTeacherById(id);
        }


    }
}
