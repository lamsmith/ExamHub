using ExamHub.Entity;
using System.ComponentModel.DataAnnotations;

namespace ExamHub.Repositories.Implementations
{
    public interface IUserRepository
    {
        User GetUserByName(string userName);
        User Authenticate(string username, string password);
       
    }
}
