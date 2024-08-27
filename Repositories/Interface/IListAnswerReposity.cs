using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IListAnswerReposity
    {
        IEnumerable<ListAnswer> GetAll();
        ListAnswer GetById(int id);
        void Add(ListAnswer listAnswer);
        void Update(ListAnswer listAnswer);
        void Delete(int id);
        void Save();
    }
}
