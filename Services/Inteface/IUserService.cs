using ExamHub.Entity;

namespace ExamHub.Services.Inteface
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
