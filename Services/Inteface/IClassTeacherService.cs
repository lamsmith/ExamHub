using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface IClassTeacherService
    {
        void CreateClassTeacher(ClassTeacher classTeacher);
        ClassTeacher GetClassTeacherById(int id);
        IEnumerable<ClassTeacher> GetAllClassTeachers();
        void UpdateClassTeacher(ClassTeacher classTeacher);
        void DeleteClassTeacher(int id);
    }
}
