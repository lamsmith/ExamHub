using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamHub.ViewModel
{
    public class TakeExamViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }

    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<OptionViewModel> Options { get; set; }
        public int SelectedOptionId { get; set; }
    }

    public class OptionViewModel
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
    }
}