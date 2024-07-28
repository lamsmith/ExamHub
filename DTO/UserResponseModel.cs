using ExamHub.Entity;
using ExamHub.Enum;

namespace ExamHub.DTO
{
    public class UserResponseModel
    {
        public int Id {  get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public int RoleId { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
