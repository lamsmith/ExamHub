using ExamHub.Entity;
using Microsoft.EntityFrameworkCore;

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
        public void UpdateTeacher(Teacher teacher);
        public void RemoveTeacherFromClass(int teacherId, int classId);
        public void RemoveTeacherFromSubject(int teacherId, int subjectId);
        public void DeleteTeacher(int teacherId);
        public Teacher GetTeacherById(int id);
       


    }
}

