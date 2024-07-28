using ExamHub.DTO;
using ExamHub.Entity;
using ExamHub.Repositories.Implementations;
using ExamHub.Repositories.Interface;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;

        public ClassService(IClassRepository classRepository, ISubjectRepository  subjectRepository)
        {
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
        }

        public IEnumerable<ClassResponseModel> GetAllClasses()
        {
            var classes =  _classRepository.GetAllClasses();
            return classes.Select(c => new ClassResponseModel
            {
                ClassName = c.ClassName,
                Id = c.Id,

            });
        }

        public ClassResponseModel GetClassById(int id)
        {
            var getClasses = _classRepository.GetClassById(id);
            return new ClassResponseModel
            {
                ClassName = getClasses.ClassName,
                Id = getClasses.Id,
                Students = getClasses.ClassStudents.Select(s => new StudentResponseModel
                {
                    FristName = s.Student.User.FirstName,
                    LastName = s.Student.User.LastName,
                }).ToList()
            };
        }

        public void CreateClass(Class newClass)
        {
            
            _classRepository.CreateClass(newClass);
        }

        public void UpdateClass(Class updatedClass)
        {
            _classRepository.UpdateClass(updatedClass);
        }

        public void DeleteClass(int id)
        {
            _classRepository.DeleteClass(id);
        }

        public IEnumerable<Class> GetAllClassTeachers(int teacherId)
        {
             return _classRepository.GetAllClassTeachers(teacherId);
        }
        public void AssignSubjectToClass(int classId, int subjectId)
        {
            var classEntity = _classRepository.GetClassById(classId);
            if (classEntity == null)
            {
                throw new KeyNotFoundException("Class not found");
            }

            var subjectEntity = _subjectRepository.GetSubjectById(subjectId);
            if (subjectEntity == null)
            {
                throw new KeyNotFoundException("Subject not found");
            }

           
            var classSubject = new ClassSubject
            {
                ClassId = classId,
                SubjectId = subjectId
            };

         
            _classRepository.AddClassSubject(classSubject);
        }
    }
}
