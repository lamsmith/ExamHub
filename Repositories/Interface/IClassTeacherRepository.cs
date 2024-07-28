using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IClassTeacherRepository
    {
        void CreateClassTeacher(ClassTeacher classTeacher);
        ClassTeacher GetClassTeacherById(int id);
        IEnumerable<ClassTeacher> GetAllClassTeachers();
        void UpdateClassTeacher(ClassTeacher classTeacher);
        void DeleteClassTeacher(int id);
    }
}
