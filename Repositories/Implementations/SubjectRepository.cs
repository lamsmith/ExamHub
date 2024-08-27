using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExamHub.Repositories.Implementations
{
    public class SubjectRepository : ISubjectRepository
    {
       
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return _context.Subjects.ToList();
        }

        public void AddSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            _context.SaveChanges();

        }
        public Subject GetSubjectById(int id)
        {
            return _context.Subjects.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateSubject(Subject updatedSubject)
        {
            var existingSubject = GetSubjectById(updatedSubject.Id);
            if (existingSubject != null)
            {
                existingSubject.SubjectName = updatedSubject.SubjectName;
                _context.SaveChanges();
            }
        }

        public void DeleteSubject(int id)
        {
            var subjectToDelete = GetSubjectById(id);
            if (subjectToDelete != null)
            {
                _context.Subjects.Remove(subjectToDelete);
                _context.SaveChanges();
            }
        }

    }
}
