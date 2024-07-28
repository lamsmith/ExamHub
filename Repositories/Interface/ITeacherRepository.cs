using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface ITeacherRepository
    {
        IEnumerable<Exam> GetExamsByTeacher(string teacherName);
        Teacher GetTeacherByName(string teacherName);
        IEnumerable<Teacher> GetAllTeachersByPrincipal();
        int GetTotalTeachers();
        public IEnumerable<ClassTeacher> GetAllClassTeachers();
        IEnumerable<SubjectTeacher> GetAllSubjectTeachers();
        public void CreateTeacher(Teacher teacher);
        void AssignTeacherToClass(int teacherId, int classId);
        void AssignTeacherToSubject(int teacherId, int subjectId);
        Teacher GetTeacherByUserId(int userId);

    }
}

