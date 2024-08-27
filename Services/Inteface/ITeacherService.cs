using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Enum;
using ExamHub.ViewModel;

namespace ExamHub.Services.Inteface
{
    public interface ITeacherService
    {
       public IEnumerable<ExamResponseModel> GetExamsByTeacher(string teacherName);
        IEnumerable<TeacherResponseModel> GetAllTeachersByPrincipal();
        int GetTotalTeachers();
        IEnumerable<ClassTeacher> GetAllClassTeachers();
        IEnumerable<SubjectTeacher> GetAllSubjectTeachers();
        Teacher GetTeacherByName(string teacherName);
        void CreateTeacher(CreateTeacherRequestModel model);
        void AssignTeacherToClassAndSubject(int teacherId, int classId, int subjectId);
        Teacher GetTeacherByUserId(int userId);
        public void UpdateTeacher(Teacher teacher);
        public void RemoveTeacherFromClass(int teacherId, int classId);
        public void RemoveTeacherFromSubject(int teacherId, int subjectId);
        public void DeleteTeacher(int teacherId);
        Teacher GetTeacherById(int id);



    }
}
