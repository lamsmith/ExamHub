using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementations
{
    public class ClassTeacherRepository : IClassTeacherRepository
    {
        private readonly ApplicationDbContext _context;


        public ClassTeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateClassTeacher(ClassTeacher classTeacher)
        {
            _context.ClassTeachers.Add(classTeacher);
            _context.SaveChanges();
        }

        public ClassTeacher GetClassTeacherById(int id)
        {
            return _context.ClassTeachers
                .Include(ct => ct.Teacher)
                .Include(ct => ct.Class)
                .FirstOrDefault(ct => ct.Id == id);
        }

        public IEnumerable<ClassTeacher> GetAllClassTeachers()
        {
            return _context.ClassTeachers
                .Include(ct => ct.Teacher)
                .Include(ct => ct.Class)
                .ToList();
        }

        public void UpdateClassTeacher(ClassTeacher classTeacher)
        {
            _context.ClassTeachers.Update(classTeacher);
            _context.SaveChanges();
        }

        public void DeleteClassTeacher(int id)
        {
            var classTeacher = _context.ClassTeachers.Find(id);
            if (classTeacher != null)
            {
                _context.ClassTeachers.Remove(classTeacher);
                _context.SaveChanges();
            }
        }
    }
}
