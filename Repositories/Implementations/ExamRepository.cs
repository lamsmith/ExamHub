using System.Collections.Generic;
using System.Linq;
using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementation
{
    public class ExamRepository : IExamRepository
    {
        
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

     

        public void AddExam(Exam exam)
        {
            //exam.Id = _context.Exams.ToList().Count + 1;
            _context.Exams.Add(exam);
            _context.SaveChanges();
        }
        public bool ExamExists(int examId)
        {
            return _context.Exams.Any(e => e.Id == examId);
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _context.Exams;
        }

        public IEnumerable<Exam> GetExamsByTeacherId(int teacherId)
        {
            return _context.Exams.Where(e => e.CreatedByTeacherId == teacherId);
        }

        public Exam GetExamById(int id)
        {
            return _context.Exams.FirstOrDefault(e => e.Id == id);
        }

        public void UpdateExam(Exam exam)
        {
            var existingExam = _context.Exams.FirstOrDefault(e => e.Id == exam.Id);
            if (existingExam != null)
            {
                existingExam.ExamName = exam.ExamName;
                existingExam.CreatedByTeacherId = exam.CreatedByTeacherId;
                existingExam.CreatedByTeacher = exam.CreatedByTeacher;
                existingExam.SubjectId = exam.SubjectId;
                existingExam.Subject = exam.Subject;
                existingExam.ClassId = exam.ClassId;
                existingExam.Class = exam.Class;
                existingExam.StartTime = exam.StartTime;
                existingExam.EndTime = exam.EndTime;
                existingExam.ExamQuestions = exam.ExamQuestions;
                existingExam.StudentExams = exam.StudentExams;
            }
        }

        public void DeleteExam(int id)
        {
            var exam = _context.Exams.FirstOrDefault(e => e.Id == id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
        }

        public IEnumerable<Exam> GetExamsByTeacher(string teacherName)
        {
            var teacher = _context.Teachers.Include(t => t.Exams)
                                           .FirstOrDefault(t => t.User.LastName == teacherName);
            return teacher?.Exams ?? new List<Exam>();
        }
        public IEnumerable<StudentExam> GetExamScoresByExamId(int examId)
        {
            return _context.StudentExams
                .Include(se => se.Student)
                .ThenInclude(s => s.User)
                .Where(se => se.ExamId == examId)
                .ToList();
        }

        public IEnumerable<ExamQuestion> GetQuestionsByExamId(int examId)
        {
            throw new NotImplementedException();
        }

        public void SaveStudentAnswer(StudentAnswer studentAnswer)
        {
           _context.StudentAnswers.Add(studentAnswer);
            _context.SaveChanges();
        } 

        public void SaveStudentExam(StudentExam studentExam)
        {
            _context.StudentExams.Add(studentExam);
            _context.SaveChanges();
        }

        //public IEnumerable<Exam> GetExamsForStudent(int studentId)
        //{
        //    return _context.Exams
        //        .Where(e => e.StudentExams.Any(se => se.StudentId == studentId))
        //        .ToList();
        //}

        public IEnumerable<Exam> GetExamsForStudent(int studentId)
        {
            return _context.Exams
                .Include(e => e.StudentExams) // Eager loading related data
                .Where(e => e.StudentExams.Any(se => se.StudentId == studentId))
                .ToList();
        }
        public IEnumerable<Exam> GetUpcomingExamsByStudent(int studentId)
        {
            var now = DateTime.Now; // Using UTC for consistency
            return _context.Exams
                .Include(e => e.StudentExams) // Eager loading related data
                .Where(e => e.DateTime > now && e.StudentExams.Any(se => se.StudentId == studentId))
                .ToList();
        }


        //public IEnumerable<Exam> GetUpcomingExamsByStudent(int studentId)
        //{
        //    var now = DateTime.Now;
        //    return _context.Exams
        //        .Where(e => e.DateTime > now && e.StudentExams.Any(se => se.StudentId == studentId))
        //        .ToList();
        //}
    }
}
