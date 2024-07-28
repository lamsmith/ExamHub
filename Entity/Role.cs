namespace ExamHub.Entity
{
    public class Role : Base
    {
        public string Name { get; set; } = default!;
        public ICollection<User> Users { get; set; } = new List<User>();

       
    }
}
