using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetAllSubjects();
        void AddSubject(Subject subject);
        Subject GetSubjectById(int id);
    }
}
