using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Data.Models;

namespace ShoppingList.Data.Services
{
    public class UserService: IUserService
    {
        private readonly ShoppingListContext _context;

        public UserService(ShoppingListContext shoppingListContext)
        {
            _context = shoppingListContext;
        }

        public async Task<bool> DoesUserExistAsync(string id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .AnyAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.CreatedOn = DateTime.UtcNow;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}