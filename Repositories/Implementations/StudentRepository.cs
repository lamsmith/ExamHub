using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace ExamHub.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
       
     


        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.Include(s => s.User).ToList();
        }

        public IEnumerable<Exam> GetAvailableExams(string studentName)
        {
            return _context.Exams.ToList();
        }

        public Student GetStudentByName(string studentName)
        {
            return _context.Students.FirstOrDefault(s => s.User.FirstName == studentName);
        }

        public int GetTotalStudents()
        {
            return _context.Students.Count();
        }
        public IEnumerable<Student> GetStudentsByClass(int classId)
        {
            return _context.Students
                                     .Include(s => s.ClassStudents)
                                     .ThenInclude(cl => cl.Class)
                                     .Include(u => u.User)
                                     .Where(s => s.ClassStudents.Any(cs => cs.ClassId == classId))
                                     .ToList();
        }

        public IEnumerable<Student> GetStudents(int teacherId)
        {
            return _context.Students
            .Where
            (s => s.ClassStudents.Any(x => x.Class.ClassTeachers
            .Any(xl => xl.TeacherId == teacherId)));
        }

        public IEnumerable<Exam> GetUpcomingExams(int studentId)
        {
             var studentClasses = _context.ClassStudents
                                         .Where(cs => cs.StudentId == studentId)
                                         .Select(cs => cs.ClassId)
                                         .ToList();

            return _context.Exams
                           .Where(e => studentClasses.Contains(e.ClassId))
                           .ToList();
        }

        public Student GetStudentById(int studentId)
        {
          return _context.Students.Include(s => s.User)
                                     .FirstOrDefault(s => s.Id == studentId);
        }

        public IEnumerable<StudentExam> GetRecentExamResults(int studentId)
        {
            return _context.StudentExams
                .Where(se => se.StudentId == studentId)
                .OrderByDescending(se => se.CompletionTime)
                .ToList();
        }

        public void AddStudent(Student student)
        {
            student.Id = _context.Students.ToList().Count + 1;
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void AssignStudentToClass(int studentId, int classId)
        {
            var assignStudentClass = new ClassStudent { StudentId = studentId, ClassId = classId };

            _context.Add(assignStudentClass);
            _context.SaveChanges();

        }

        public Student GetStudentByUserId(int userId)
        {
            return _context.Students.Include(s => s.User)
                .Include(s => s.ClassStudents)
            .ThenInclude( cs => cs.Class)
                .FirstOrDefault(s => s.UserId == userId);
        }
    }
}
