using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface IClassService
    {
        IEnumerable<ClassResponseModel> GetAllClasses();
        ClassResponseModel GetClassById(int id);
        void CreateClass(Class newClass);
        void UpdateClass(Class updatedClass);
        void DeleteClass(int id);
        IEnumerable<Class> GetAllClassTeachers(int teacherId);
        void AssignSubjectToClass(int classId, int subjectId);
        public int GetTotalClasses();
    }
}
