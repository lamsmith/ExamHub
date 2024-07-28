using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class NotificationResponseModel
    {
        public int Id {  get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsRead { get; set; }
        public DateTime DateCreated { get; internal set; }
    }
}
