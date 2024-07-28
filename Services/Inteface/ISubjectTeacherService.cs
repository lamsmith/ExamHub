using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface ISubjectTeacherService
    {
        void CreateSubjectTeacher(SubjectTeacher subjectTeacher);
        SubjectTeacher GetSubjectTeacherById(int id);
        IEnumerable<SubjectTeacher> GetAllSubjectTeachers();
        void UpdateSubjectTeacher(SubjectTeacher subjectTeacher);
        void DeleteSubjectTeacher(int id);
    }
}
