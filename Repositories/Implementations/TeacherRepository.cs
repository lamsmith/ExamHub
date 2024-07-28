using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementations
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
       

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Teacher> GetAllTeachersByPrincipal()
        {
            var response = _context.Teachers.Include(t => t.User).Include(t => t.ClassTeachers).Include(t => t.SubjectTeachers).ToList();
            return response;
        }

        public IEnumerable<Exam> GetExamsByTeacher(string teacherName)
        {
            return _context.Exams.Where(e => e.CreatedByTeacher.User.FirstName == teacherName).ToList();
        }

        public Teacher GetTeacherByName(string teacherName)
        {
            return _context.Teachers.Include(st => st.SubjectTeachers).ThenInclude(st => st.Subject).Include(ct => ct.ClassTeachers).ThenInclude(cl => cl.Class).FirstOrDefault(t => t.User.Username == teacherName);
        }

        public int GetTotalTeachers()
        {
            return _context.Teachers.Count();
        }
       
        public IEnumerable<ClassTeacher> GetAllClassTeachers()
        {
            return _context.ClassTeachers.Include(ct => ct.Class).Include(ct => ct.Teacher).ToList();
        }

        public IEnumerable<SubjectTeacher> GetAllSubjectTeachers()
        {
            return _context.SubjectTeachers.Include(st => st.Subject).Include(st => st.Teacher).ToList();
        }

        public void CreateTeacher(Teacher teacher)
        {
            teacher.Id = _context.Teachers.ToList().Count + 1;
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
        }
        public void AssignTeacherToClass(int teacherId, int classId)
        {
            var classTeacherAssignment = new ClassTeacher { TeacherId = teacherId, ClassId = classId };
            _context.ClassTeachers.Add(classTeacherAssignment);
            _context.SaveChanges();
        }

        public void AssignTeacherToSubject(int teacherId, int subjectId)
        {
            var subjectTeacherAssignment = new SubjectTeacher { TeacherId = teacherId, SubjectId = subjectId };
            _context.SubjectTeachers.Add(subjectTeacherAssignment);
            _context.SaveChanges();
        }
        public Teacher GetTeacherByUserId(int userId)
        {
            return _context.Teachers.Include(t => t.User).FirstOrDefault(t => t.UserId == userId);
        }


    }
}
