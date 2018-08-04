using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingList.Data.Models;

namespace ShoppingList.Data.Services
{
    public class GroupService
    {
        private readonly ShoppingListContext _context;

        public GroupService(ShoppingListContext shoppingListContext)
        {
            _context = shoppingListContext;
        }

        public async Task<Group> CreateGroupAsync(string name, User owner)
        {
            var groupToCreate = new Group
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                CreatedOn = DateTime.UtcNow,
                Users = new List<User> { owner }
            };

            await _context.Groups.AddAsync(groupToCreate);
            await _context.SaveChangesAsync();
            return groupToCreate;
        }

        public IEnumerable<Group> GetGroups(User user)
        {
            return _context.Groups
                .Where(g => g.Users.Exists(u => u.Id == user.Id));
        }
    }
}