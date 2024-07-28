using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamHub.Entity
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
    }
}