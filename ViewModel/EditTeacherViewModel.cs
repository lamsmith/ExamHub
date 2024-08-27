using ExamHub.DTO;
using ExamHub.Entity;

namespace ExamHub.ViewModel
{
    public class EditTeacherViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> AssignedClassIds { get; set; }
        public List<int> AssignedSubjectIds { get; set; }
        public List<ClassResponseModel> AllClasses { get; set; }
        public List<SubjectResponseModel> AllSubjects { get; set; }
        public int NewClassId { get; set; }
        public int NewSubjectId { get; set; }
        public int RemovedClassId { get; set; }
        public int RemovedSubjectId { get; set; }
    }
}
