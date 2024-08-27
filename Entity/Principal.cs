namespace ExamHub.Entity
{
    public class Principal : Base
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
