    using System.ComponentModel.DataAnnotations;

    namespace ExamHub.DTO
    {
    public class CreateExamQuestionRequestModel
    {
        public int ExamId { get; set; }
        public int QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public int QuestionId { get; set; } 
        public List<UploadedQuestion> UploadedQuestions { get; set; } = new List<UploadedQuestion>();
    }

    public class UploadedQuestion
    {
          
        public string QuestionText { get; set; }
        public int QuestionNo { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
    }

}


