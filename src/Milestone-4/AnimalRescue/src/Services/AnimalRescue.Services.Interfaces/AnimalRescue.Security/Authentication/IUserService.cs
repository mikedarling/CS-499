using System.Threading.Tasks;

namespace AnimalRescue.Security.Authentication
{
    public interface IUserService
    {

        Task<bool> AttemptLogin(string username, string password);

        bool Logout();

    }
}
