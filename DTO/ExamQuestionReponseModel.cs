using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class ExamQuestionReponseModel
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public ICollection<OptionResponseModel> Options { get; set; } = new List<OptionResponseModel>();
        public string QuestionText { get; set; }
        //public ICollection<ExamQuestionAnswer> Answers { get; set; } = new List<ExamQuestionAnswer>();
        public string CorrectAnswer { get; set; }
    }
}
