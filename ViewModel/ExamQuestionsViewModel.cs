using System.ComponentModel.DataAnnotations;

namespace ExamHub.ViewModel
{
    public class ExamQuestionsViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        public int QuestionNo { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Question text can't be longer than 500 characters.")]
        public string QuestionText { get; set; }

        public ICollection<OptionViewModel> Options { get; set; } = new List<OptionViewModel>();

        [Required]
        public int CorrectAnswer { get; set; }

        // Optional: if you need to include student answers or other related data
        // public ICollection<StudentAnswerViewModel> StudentAnswers { get; set; } = new List<StudentAnswerViewModel>();
    }

   
    // Optional: Define StudentAnswerViewModel if needed
    public class StudentAnswerViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SelectedOptionId { get; set; }
        public string SelectedOptionText { get; set; }
    }
}
