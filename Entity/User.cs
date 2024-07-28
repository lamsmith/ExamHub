using ExamHub.Enum;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace ExamHub.Entity
{
    public class User : Base
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Role Role { get; set; } = default!;
        public int RoleId { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

      
      
    
    }
}
