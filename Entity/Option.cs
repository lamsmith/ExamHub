using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamHub.Entity
{
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = default!;
        public string OptionLabel { get; set; }
        public int ExamQuestionId { get; set; }
        public ExamQuestion ExamQuestion { get; set; }
    }
}