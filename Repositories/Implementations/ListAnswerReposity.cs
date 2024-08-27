using ExamHub.Context;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;

namespace ExamHub.Repositories.Implementations
{
    public class ListAnswerReposity : IListAnswerReposity
    {
        private readonly ApplicationDbContext _context;

        public ListAnswerReposity(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ListAnswer> GetAll()
        {
            return _context.ListAnswers.ToList();
        }

        public ListAnswer GetById(int id)
        {
            return _context.ListAnswers.Find(id);
        }

        public void Add(ListAnswer listAnswer)
        {
            _context.ListAnswers.Add(listAnswer);
            Save();
        }

        public void Update(ListAnswer listAnswer)
        {
            _context.ListAnswers.Update(listAnswer);
            Save();
        }

        public void Delete(int id)
        {
            var listAnswer = _context.ListAnswers.Find(id);
            if (listAnswer != null)
            {
                _context.ListAnswers.Remove(listAnswer);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
