using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;

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
                if (!IsTokenPayloadValid(payload)) throw new Exception();
            }
            catch
            {
                context.Response.StatusCode = 401;
                return;
            }

  
            await _next(context);
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