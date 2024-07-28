namespace ExamHub.Entity
{
    public class Principal : Base
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
