using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamHub.Entity
{
    public class ExamQuestion
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int QuestionNo { get; set; }

        public string QuestionText { get; set; }
        public ICollection<Option> Options { get; set; } =  new List<Option>();

        public string CorrectAnswer { get; set; }
        //public string PickedAnswer { get; set; }
    }
}
