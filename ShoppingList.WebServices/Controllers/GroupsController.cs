using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data.Models;
using ShoppingList.Data.Services;
using ShoppingList.WebServices.UserRegistration;

namespace ShoppingList.WebServices.Controllers
{
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly GroupService _groupService;

        public GroupsController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public IEnumerable<Group> GetGroups()
        {
            return _groupService.GetGroups(HttpContext.GetShoppingListUser());
        }

        [HttpPost("{groupName}")]
        public async Task<Group> CreateGroupAsync(string groupName)
        {
            var group = await _groupService.CreateGroupAsync(groupName, HttpContext.GetShoppingListUser());
            return group;
        }
    }
}