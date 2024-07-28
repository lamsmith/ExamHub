using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementations
{
    public class SubjectTeacherRepository : ISubjectTeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectTeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            _context.SubjectTeachers.Add(subjectTeacher);
            _context.SaveChanges();
        }

        public SubjectTeacher GetSubjectTeacherById(int id)
        {
            return _context.SubjectTeachers
                .Include(st => st.Teacher)
                .Include(st => st.Subject)
                .FirstOrDefault(st => st.Id == id);
        }

        public IEnumerable<SubjectTeacher> GetAllSubjectTeachers()
        {
            return _context.SubjectTeachers
                .Include(st => st.Teacher)
                .Include(st => st.Subject)
                .ToList();
        }

        public void UpdateSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            _context.SubjectTeachers.Update(subjectTeacher);
            _context.SaveChanges();
        }

        public void DeleteSubjectTeacher(int id)
        {
            var subjectTeacher = _context.SubjectTeachers.Find(id);
            if (subjectTeacher != null)
            {
                _context.SubjectTeachers.Remove(subjectTeacher);
                _context.SaveChanges();
            }
        }
    }
}
