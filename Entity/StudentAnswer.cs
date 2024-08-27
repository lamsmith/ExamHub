using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamHub.Entity
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public ExamQuestion ExamQuestion { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SelectedOptionId { get; set; }
        public ListAnswer ListAnswer { get; set; }
        public int ListAnswerId { get; set; }

    }

}
