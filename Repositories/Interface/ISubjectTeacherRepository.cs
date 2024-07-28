using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface ISubjectTeacherRepository
    {
        void CreateSubjectTeacher(SubjectTeacher subjectTeacher);
        SubjectTeacher GetSubjectTeacherById(int id);
        IEnumerable<SubjectTeacher> GetAllSubjectTeachers();
        void UpdateSubjectTeacher(SubjectTeacher subjectTeacher);
        void DeleteSubjectTeacher(int id);
    }
}
