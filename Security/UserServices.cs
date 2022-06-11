using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto_ing_software_api.Security
{
    public class UserServices : IUserServices
    {
        


        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));
            if (user == null)
                return null;
            return user;
        }

        
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await Task.Run(() => _users);
        }





    }
}