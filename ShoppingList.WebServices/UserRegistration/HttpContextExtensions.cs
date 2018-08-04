using Microsoft.AspNetCore.Http;
using ShoppingList.Data.Models;

namespace ShoppingList.WebServices.UserRegistration
{
    public static class HttpContextExtensions
    {
        public static User GetShoppingListUser(this HttpContext httpContext)
        {
            return (httpContext.User as ShoppingListUser).User;
        }
    }
}