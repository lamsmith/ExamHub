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

        public void UpdateTeacher(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
        }

        public void RemoveTeacherFromClass(int teacherId, int classId)
        {
            var classTeacher = _context.ClassTeachers.FirstOrDefault(ct => ct.TeacherId == teacherId && ct.ClassId == classId);
            if (classTeacher != null)
            {
                _context.ClassTeachers.Remove(classTeacher);
                _context.SaveChanges();
            }
        }

        public void RemoveTeacherFromSubject(int teacherId, int subjectId)
        {
            var subjectTeacher = _context.SubjectTeachers.FirstOrDefault(st => st.TeacherId == teacherId && st.SubjectId == subjectId);
            if (subjectTeacher != null)
            {
                _context.SubjectTeachers.Remove(subjectTeacher);
                _context.SaveChanges();
            }
        }

        public void DeleteTeacher(int teacherId)
        {
            var teacher = _context.Teachers.Include(t => t.User).FirstOrDefault(t => t.Id == teacherId);
            if (teacher != null)
            {
                _context.Users.Remove(teacher.User); // Assuming cascading deletes handle related entities
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
            }
        }

        public Teacher GetTeacherById(int id)
        {
            return _context.Teachers.Include(t => t.User)
                                    .Include(t => t.ClassTeachers).ThenInclude(ct => ct.Class)
                                    .Include(t => t.SubjectTeachers).ThenInclude(st => st.Subject)
                                    .FirstOrDefault(t => t.Id == id);
        }


    }
}
