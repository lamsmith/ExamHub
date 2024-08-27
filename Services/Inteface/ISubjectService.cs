using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface ISubjectService
    {
        IEnumerable<SubjectResponseModel> GetAllSubjects();
        public void CreateSubject(Subject subject);
        public void UpdateSubject(int id, string newName);
        public void DeleteSubject(int id);
    }

}
