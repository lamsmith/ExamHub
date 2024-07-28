using ExamHub.Entity;

namespace ExamHub.DTO
{
    public class RoleResponseModel
    {
        public string Name { get; set; } = default!;
        public ICollection<UserResponseModel> Users { get; set; } = new List<UserResponseModel>();
    }
}
