using System.Collections.Generic;
using System.Linq;
using ExamHub.Context;
using ExamHub.DTO;
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

        public void SaveExamResult(ExamResult examResult)
        {
            _context.ExamResults.Add(examResult);
            _context.SaveChanges();
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
            return _context.ExamQuestions
                .Include(q => q.Options)
                .Where(q => q.ExamId == examId)
                .ToList();
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

        public IEnumerable<Exam> GetExamsForStudent(int classId)
        {
            var today = DateTime.Today;

            var examForStudent = _context.Exams

                .Include(e => e.Subject)
                .Where(e => e.ClassId == classId && e.StartTime.Date == today)
                .ToList();

            return examForStudent;


        }

        public IEnumerable<Exam> GetUpcomingExamsByClass(int classId)
        {
            var now = DateTime.Now;

           
            var upcomingExams = _context.Exams
                .Include(e => e.Subject) 
                .Where(e => e.ClassId == classId && e.EndTime > now)
                .ToList();

            return upcomingExams;
        }

        public List<ExamQuestion> GetExamQuestionsForExam(int examId)
        {
            return _context.ExamQuestions.Where(eq => eq.ExamId == examId).ToList();
        }

        public List<StudentAnswer> GetStudentAnswersForExam(int studentId, int examId)
        {
            return _context.StudentAnswers
                .Where(sa => sa.StudentId == studentId && sa.ExamQuestion.ExamId == examId)
                .ToList();
        }

        public IEnumerable<StudentAnswerResponseModel> GetSelectedOptionTextsForExam(int examId, int studentId)
        {
            var result = from sa in _context.StudentAnswers
                         join q in _context.ExamQuestions on sa.QuestionId equals q.Id
                         join o in _context.Options on sa.SelectedOptionId equals o.Id
                         where sa.StudentId == studentId && q.ExamId == examId
                         select new StudentAnswerResponseModel
                         {
                             QuestionId = q.Id,
                             SelectedOptionText = o.OptionText
                         };

            return result.ToList();
        }

        public ExamQuestion GetExamQuestionById(int id)
        {
            return _context.ExamQuestions.Include(q => q.Options).FirstOrDefault(q => q.Id == id);
        }

        public void UpdateExamQuestion(ExamQuestion examQuestion)
        {
            var existingQuestion = _context.ExamQuestions.Include(q => q.Options).FirstOrDefault(q => q.Id == examQuestion.Id);
            if (existingQuestion != null)
            {
                existingQuestion.QuestionNo = examQuestion.QuestionNo;
                existingQuestion.QuestionText = examQuestion.QuestionText;
                existingQuestion.Options = examQuestion.Options;
                existingQuestion.CorrectAnswer = examQuestion.CorrectAnswer;

            }
        }

        public void DeleteExamQuestion(int id)
        {
            var question = _context.ExamQuestions.FirstOrDefault(q => q.Id == id);
            if (question != null)
            {
                _context.ExamQuestions.Remove(question);
            }
        }

    }


}
