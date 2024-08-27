namespace ExamHub.ViewModel
{
    public class EditExamQuestionViewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public List<OptionViewModel> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }

    
}
