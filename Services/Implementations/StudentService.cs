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
        private readonly IClassRepository _classRepository;


        public StudentService(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }

        public IEnumerable<StudentResponseModel> GetAllStudent()
        {
            var getAllStudent = _studentRepository.GetAllStudents();
            return getAllStudent.Select(x => new StudentResponseModel
            {
                Id = x.Id,
                FirstName = x.User.FirstName,
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
                CreatedAt = DateTime.Now,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
              
            });
        }

        public IEnumerable<StudentResponseModel> GetStudentsByClass(int classId)
        {
            List<Student> students = classId == 0 ? _studentRepository.GetAllStudents().ToList() : _studentRepository.GetStudentsByClass(classId).ToList();

            return students.Select(s => new StudentResponseModel
            {
                Id = s.Id,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                UserName = s.User.Username,
                ClassName = (s.ClassStudents.Select(cs => cs.Class).Count() > 0 ) ? s.ClassStudents.Select(cs => cs.Class).ToList()[0].ClassName : ""
            });
        }

        public IEnumerable<StudentResponseModel> GetStudents(int teacherId)
        {
            var students = _studentRepository.GetStudents(teacherId);
            return students.Select(s => new StudentResponseModel
            {
                Id = s.Id,
                FirstName = s.User.FirstName,
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
                CreatedAt = DateTime.Now,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
              
            });
        }
         public BaseResponse CreateStudent(CreateStudentRequestModel model)
        {
            var existingStudent = _studentRepository.GetStudentByUserName(model.UserName);
            if (existingStudent != null) {
                return new BaseResponse { Message = "There's an existing user with the user name", Status = false };
            }
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
            var studentclass = _classRepository.GetClassById(model.ClassId);
            student.ClassStudents.Add(new ClassStudent
            {
                Class = studentclass
            });
            _studentRepository.AddStudent(student);
            _studentRepository.AssignStudentToClass(student.Id, model.ClassId);
            return new BaseResponse { Message = "Student created successfully", Status = true };
        }

        public void AssignStudentToClass(int studentId, int classId)
        {
            _studentRepository.AssignStudentToClass(studentId, classId);
        }

        public Student GetStudentByUserId(int userId)
        {
           return  _studentRepository.GetStudentByUserId(userId);
        }

        public IEnumerable<int> GetStudentAnswersForExam(int studentId, int examId)
        {
            return _studentRepository.GetStudentAnswersForExam(studentId, examId)
                .Select(sa => sa.SelectedOptionId)
                .ToList();
        }

    }
}

