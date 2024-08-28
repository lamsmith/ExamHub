using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExamHub.Repositories.Implementations
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }
       

        public IEnumerable<Class> GetAllClasses()
        {
            return _context.Classes.ToList();
        }

        public Class GetClassById(int id)
        {
            return _context.Classes.FirstOrDefault(c => c.Id == id);
        }

        public void CreateClass(Class newClass)
        {
            if (newClass.CreatedByPrincipalId == 0)
            {
                throw new Exception("CreatedByPrincipalId is not set.");
            }
            _context.Classes.Add(newClass);
            _context.SaveChanges();  
        }

        public void UpdateClass(Class updatedClass)
        {
            var existingClass = GetClassById(updatedClass.Id);
            if (existingClass != null)
            {
                existingClass.ClassName = updatedClass.ClassName;
                existingClass.ClassStudents = updatedClass.ClassStudents;
                _context.SaveChanges(); 
            }
        }

        public void DeleteClass(int id)
        {
            var classToDelete = GetClassById(id);
            if (classToDelete != null)
            {
                _context.Classes.Remove(classToDelete);
                _context.SaveChanges();  
            }
        }


        public IEnumerable<Class> GetAllClassTeachers(int teacherId)
        {
           return _context.Classes.Where(cl => cl.ClassTeachers.Any(ct => ct.Teacher.Id == teacherId));
        }

        public void AddClassSubject(ClassSubject classSubject)
        {
            _context.ClassSubjects.Add(classSubject);
            _context.SaveChanges();
        }

        public int GetTotalClasses()
        {
            return _context.Classes.Count();
        }
    }
}

