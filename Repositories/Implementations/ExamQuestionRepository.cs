using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementations
{
    public class ExamQuestionRepository : IExamQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ExamQuestion> GetAllExamQuestions()
        {
            return _context.ExamQuestions.ToList();
        }

        public ExamQuestion GetExamQuestionById(int id)
        {
            return
                _context.ExamQuestions
                .Include(eq => eq.Options)
                .FirstOrDefault(eq => eq.Id == id);
        }

        public void AddExamQuestion(ExamQuestion examQuestion)
        {
            _context.ExamQuestions.Add(examQuestion);
            _context.SaveChanges();
        }

        public void UpdateExamQuestion(ExamQuestion examQuestion)
        {
            _context.ExamQuestions.Update(examQuestion);
            _context.SaveChanges();
        }

        public void DeleteExamQuestion(int id)
        {
            var examQuestion = _context.ExamQuestions.FirstOrDefault(eq => eq.Id == id);
            if (examQuestion != null)
            {
                _context.ExamQuestions.Remove(examQuestion);
                _context.SaveChanges();
            }
        }
        public IEnumerable<int> GetCorrectAnswersForExam(int examId)
        {
                  
            var correctAnswers = _context.ExamQuestions
                .Where(q => q.ExamId == examId)
                .SkipWhile(q => q == null )
                .Select(q => q.CorrectAnswer)
                .ToList();

         
            return correctAnswers;
        }




        public IEnumerable<ExamQuestion> GetQuestionsForExam(int examId)
        {
            return _context.ExamQuestions
                .Where(eq => eq.ExamId == examId)
                .Include(eq => eq.Options) // Include options if needed
                .ToList();
        }

    }
}
