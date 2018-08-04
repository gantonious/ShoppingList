using System.Security.Claims;
using ShoppingList.Data.Models;

namespace ShoppingList.WebServices.UserRegistration
{
    public class ShoppingListUser : ClaimsPrincipal
    {
        public User User { get; set; }
    }
}