using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IClassRepository
    {

        IEnumerable<Class> GetAllClasses();
        Class GetClassById(int id);
        void CreateClass(Class newClass);
        void UpdateClass(Class updatedClass);
        void DeleteClass(int id);
        IEnumerable<Class> GetAllClassTeachers(int teacherId);
        void AddClassSubject(ClassSubject classSubject);
    }
}
