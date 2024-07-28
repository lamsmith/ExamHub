using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
    public class SubjectTeacherService : ISubjectTeacherService
    {
        private readonly ISubjectTeacherRepository _subjectTeacherRepository;

        public SubjectTeacherService(ISubjectTeacherRepository subjectTeacherRepository)
        {
            _subjectTeacherRepository = subjectTeacherRepository;
        }

        public void CreateSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            _subjectTeacherRepository.CreateSubjectTeacher(subjectTeacher);
        }

        public SubjectTeacher GetSubjectTeacherById(int id)
        {
            return _subjectTeacherRepository.GetSubjectTeacherById(id);
        }

        public IEnumerable<SubjectTeacher> GetAllSubjectTeachers()
        {
            return _subjectTeacherRepository.GetAllSubjectTeachers();
        }

        public void UpdateSubjectTeacher(SubjectTeacher subjectTeacher)
        {
            _subjectTeacherRepository.UpdateSubjectTeacher(subjectTeacher);
        }

        public void DeleteSubjectTeacher(int id)
        {
            _subjectTeacherRepository.DeleteSubjectTeacher(id);
        }
    }
}
