using System.Threading.Tasks;
using ShoppingList.Data.Models;

namespace ShoppingList.Data.Services
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string id);
        Task<User> CreateUserAsync(User user);
    }
}