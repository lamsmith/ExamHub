using ExamHub.Entity;
using ExamHub.Repositories.Implementations;
using ExamHub.Services.Inteface;

namespace ExamHub.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Authenticate(string username, string password)
        {
            return _repository.Authenticate(username, password);
        }
    }
}
