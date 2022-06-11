using System.Collections.Generic;
using System.Threading.Tasks;

namespace proyecto_ing_software_api.Security
{
    public interface IUserServices
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAllUsers();
    }
}