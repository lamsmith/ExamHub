namespace ExamHub.Entity
{
    public class Subject :Base
    {
        public string SubjectName { get; set; }
        public Principal Principal { get; set; }
        public int PrincipalId { get; set; }
        public ICollection<SubjectTeacher> SubjectTeachers { get; set; }
        public ICollection<SubjectStudent> SubjectStudents{ get; set; }
        public ICollection<ClassSubject> ClassSubjects { get; set; }
        public ICollection<Exam> Exams { get; set; }

 
    }
}
