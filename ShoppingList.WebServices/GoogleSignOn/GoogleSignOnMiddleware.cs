using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace ShoppingList.WebServices.GoogleSignOn
{
    public class GoogleSignOnMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GoogleSignOnOptions _options;
 
        public GoogleSignOnMiddleware(RequestDelegate next, GoogleSignOnOptions options)
        {
            _next = next;
            _options = options;
        }
 
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var idToken = GetIdTokenFrom(context);
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                
                if (IsTokenPayloadValid(payload))
                {
                    context.User = new GoogleUser {UserPayload = payload};
                    await _next(context);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                context.Response.StatusCode = 401;
            }
        }

        private static string GetIdTokenFrom(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"];
            return authHeader[0].Split("Bearer ")[1];
        }

        private bool IsTokenPayloadValid(GoogleJsonWebSignature.Payload payload)
        {
            return payload != null &&
                   _options.ClientIds.Contains(payload.Audience);
        }
    }
}