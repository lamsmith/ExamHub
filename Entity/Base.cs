namespace ExamHub.Entity
{
    public abstract class Base
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string?  ModifiedBy  { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
