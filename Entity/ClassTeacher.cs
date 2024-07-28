using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamHub.Entity
{
    public class ClassTeacher
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
