using ExamHub.Entity;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
    public class ClassTeacherService : IClassTeacherService
    {
        private readonly IClassTeacherRepository _classTeacherRepository;

        public ClassTeacherService(IClassTeacherRepository classTeacherRepository)
        {
            _classTeacherRepository = classTeacherRepository;
        }

        public void CreateClassTeacher(ClassTeacher classTeacher)
        {
            _classTeacherRepository.CreateClassTeacher(classTeacher);
        }

        public ClassTeacher GetClassTeacherById(int id)
        {
            return _classTeacherRepository.GetClassTeacherById(id);
        }

        public IEnumerable<ClassTeacher> GetAllClassTeachers()
        {
            return _classTeacherRepository.GetAllClassTeachers();
        }

        public void UpdateClassTeacher(ClassTeacher classTeacher)
        {
            _classTeacherRepository.UpdateClassTeacher(classTeacher);
        }

        public void DeleteClassTeacher(int id)
        {
            _classTeacherRepository.DeleteClassTeacher(id);
        }
    }
}
