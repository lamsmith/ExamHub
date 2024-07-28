using ExamHub.Context;
using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Enum;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExamHub.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<StudentResponseModel> GetAllStudent()
        {
            var getAllStudent = _studentRepository.GetAllStudents();
            return getAllStudent.Select(x => new StudentResponseModel
            {
                Id = x.Id,
                FristName = x.User.FirstName,
                LastName = x.User.LastName,
            });
        }

        public IEnumerable<ExamResponseModel> GetAvailableExams(string studentName)
        {
            var exams = _studentRepository.GetAvailableExams(studentName);
            return exams.Select(e => new ExamResponseModel
            {
                Id = e.Id,
                ExamName = e.ExamName,
                DateTime = e.DateTime,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
              
            });
        }

        public IEnumerable<StudentResponseModel> GetStudentsByClass(int classId)
        {
            var getStudentByClass = _studentRepository.GetStudentsByClass(classId);
            return getStudentByClass.Select(s => new StudentResponseModel
            {
                Id = s.Id,
                FristName = s.User.FirstName,
                LastName = s.User.LastName,
                Class = s.ClassStudents.Select(cs => cs.Class).ToList(),             
            });
        }

        public IEnumerable<StudentResponseModel> GetStudents(int teacherId)
        {
            var students = _studentRepository.GetStudents(teacherId);
            return students.Select(s => new StudentResponseModel
            {
                Id = s.Id,
                FristName = s.User.FirstName,
                LastName = s.User.LastName,
            });
        }

        public int GetTotalStudents()
        {
            return _studentRepository.GetTotalStudents();
        }

        public Student GetStudentById(int studentId)
        {
            return _studentRepository.GetStudentById(studentId);
        }

        public IEnumerable<ExamResponseModel> GetUpcomingExams(int studentId)
        {
            var exams = _studentRepository.GetUpcomingExams(studentId);
            return exams.Select(e => new ExamResponseModel
            {
                Id = e.Id,
                ExamName = e.ExamName,
                DateTime = e.DateTime,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
              
            });
        }
         public void CreateStudent(CreateStudentRequestModel model)
        {
            var user = new User
            {
                Username = model.UserName,
                Password = model.Password,
                CreatedAt = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                RoleId = 3,
                CreatedBy = "1",
            };

            var student = new Student
            {
                
                User = user,
                CreatedAt = DateTime.Now,
                CreatedBy = "1",
            };

            var classstudent = new ClassStudent
            {
                StudentId = student.Id,
                Student = student,
                ClassId = model.ClassId,
            };       

            _studentRepository.AddStudent(student);
            _studentRepository.AssignStudentToClass(student.Id, model.ClassId);
        }

        public void AssignStudentToClass(int studentId, int classId)
        {
            _studentRepository.AssignStudentToClass(studentId, classId);
        }

        public Student GetStudentByUserId(int userId)
        {
           return  _studentRepository.GetStudentByUserId(userId);
        }
    }
}

