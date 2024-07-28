using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementations
{
    public class ClassStudentRepository : IClassStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ClassStudent classStudent)
        {
            _context.ClassStudents.Add(classStudent);
            _context.SaveChanges();
        }

        public void Remove(ClassStudent classStudent)
        {
            _context.ClassStudents.Remove(classStudent);
            _context.SaveChanges();
        }

        public IEnumerable<ClassStudent> GetAll()
        {
            return _context.ClassStudents
                .Include(cs => cs.Class)
                .Include(cs => cs.Student)
                .ToList();
        }

        public ClassStudent GetById(int id)
        {
            return _context.ClassStudents
                .Include(cs => cs.Class)
                .Include(cs => cs.Student)
                .FirstOrDefault(cs => cs.Id == id);
        }

        public IEnumerable<ClassStudent> GetByClassId(int classId)
        {
            return _context.ClassStudents
                .Include(cs => cs.Class)
                .Include(cs => cs.Student)
                .Where(cs => cs.ClassId == classId)
                .ToList();
        }

        public IEnumerable<ClassStudent> GetByStudentId(int studentId)
        {
            return _context.ClassStudents
                .Include(cs => cs.Class)
                .Include(cs => cs.Student)
                .Where(cs => cs.StudentId == studentId)
                .ToList();
        }
    }
}

