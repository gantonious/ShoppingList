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
        private readonly UserService _userService;
 
        public UserRegistrationMiddlware(RequestDelegate next, UserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {
            var user = context.User as GoogleUser;
            
            if (user == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var doesUserExist = await _userService.DoesUserExistAsync(user.UserPayload.Email);

            if (!doesUserExist)
            {
                var newUser = new User
                {
                    Id = user.UserPayload.Email,
                    Name = user.UserPayload.Name,
                    EmailAddress = user.UserPayload.Email,
                    ProfileUrl = user.UserPayload.Picture
                };

                await _userService.CreateUserAsync(newUser);
            }

            await _next(context);
        }
    }
}