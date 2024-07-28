namespace ExamHub.ViewModel
{
    public class ClassStudentsViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }

    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Username { get; set; }
     
    }
}
