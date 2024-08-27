
using System.ComponentModel.DataAnnotations;

namespace ExamHub.ViewModel
{
    public class ExamQuestionViewModel
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int QuestionNo { get; set; }
        public string QuestionText { get; set; }

        public ICollection<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();

        public int CorrectAnswer { get; set; }
    }
}
