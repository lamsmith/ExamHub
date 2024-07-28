using ExamHub.Entity;

namespace ExamHub.Repositories.Interface
{
    public interface IClassStudentRepository
    {
        void Add(ClassStudent classStudent);
        void Remove(ClassStudent classStudent);
        IEnumerable<ClassStudent> GetAll();
        ClassStudent GetById(int id);
        IEnumerable<ClassStudent> GetByClassId(int classId);
        IEnumerable<ClassStudent> GetByStudentId(int studentId);
    }
}
