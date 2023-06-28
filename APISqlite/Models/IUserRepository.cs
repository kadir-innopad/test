using APISqlite.Data;
using System.Xml.Linq;

namespace APISqlite.Models
{
    public interface IUserRepository
    {
        List<User> GetUserlist();

        Task<User> GetUserById(int id);

        Task<User> DeleteUser(int id);  
        Task  UpdateUser(User user);
        Task<int> CreateUser(User user);

       string Authenticate (String username, string email);
    }
}
