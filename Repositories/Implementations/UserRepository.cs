using ExamHub.Context;
using ExamHub.Entity;
using Microsoft.EntityFrameworkCore;

namespace ExamHub.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            return _context.Users.Include(x => x.Role).SingleOrDefault(u => u.Username == username && u.Password == password);
        }
        public User GetUserByName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.Username == userName);
        }

    }
}
