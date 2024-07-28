using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;

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
            return _context.ExamQuestions.FirstOrDefault(eq => eq.Id == id);
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


    }
}
