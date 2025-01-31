﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamHub.Entity
{
    public class Exam :Base
    {
        public string ExamName { get; set; }
        public int CreatedByTeacherId { get; set; }
        public Teacher CreatedByTeacher { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
        public DateTime StartTime { get; set; }    
        public DateTime EndTime { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
        public ICollection<ListAnswer> listanswers { get; set; } = new List<ListAnswer>();
    }
}
