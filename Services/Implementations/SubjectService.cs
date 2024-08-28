using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public IEnumerable<SubjectResponseModel> GetAllSubjects()
        {
            var subjects = _subjectRepository.GetAllSubjects();
            return subjects.Select(s => new SubjectResponseModel
            {
                Id = s.Id,
                SubjectName = s.SubjectName,

            });
        }

        public void CreateSubject(Subject subject)
        {
            _subjectRepository.AddSubject(subject);
        }

        public void UpdateSubject(int id, string newName)
        {
            var subject = _subjectRepository.GetSubjectById(id);
            if (subject != null)
            {
                subject.SubjectName = newName;
                _subjectRepository.UpdateSubject(subject);
            }
        }

        public void DeleteSubject(int id)
        {
            _subjectRepository.DeleteSubject(id);
        }

    }
}
