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

        public void CreateSubject(string name)
        {
            var subject = new Subject { Id = _subjectRepository.GetAllSubjects().Count() + 1, SubjectName = name };
            _subjectRepository.AddSubject(subject);
        }
    }
}
