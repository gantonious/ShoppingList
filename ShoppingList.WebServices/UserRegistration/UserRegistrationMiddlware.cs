using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShoppingList.Data.Models;
using ShoppingList.Data.Services;
using ShoppingList.WebServices.GoogleSignOn;

namespace ShoppingList.WebServices.UserRegistration
{
    public class UserRegistrationMiddlware
    {
        private readonly RequestDelegate _next;
 
        public UserRegistrationMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var user = context.User as GoogleUser;
            
            if (user == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var databaseUser = await userService.GetUserAsync(user.UserPayload.Email);

            if (databaseUser == null)
            {
                var newUser = new User
                {
                    Id = user.UserPayload.Email,
                    Name = user.UserPayload.Name,
                    EmailAddress = user.UserPayload.Email,
                    ProfileUrl = user.UserPayload.Picture
                };

                await userService.CreateUserAsync(newUser);
                context.User = new ShoppingListUser { User = newUser };
            }

            context.User = new ShoppingListUser {User = databaseUser};
            await _next(context);
        }
    }
}