using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface ISubjectService
    {
        IEnumerable<SubjectResponseModel> GetAllSubjects();
        public void CreateSubject(string name);
    }
}
