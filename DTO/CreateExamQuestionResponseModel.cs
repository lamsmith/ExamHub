namespace ExamHub.DTO
{
    public class CreateExamQuestionResponseModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }     
    }
}
