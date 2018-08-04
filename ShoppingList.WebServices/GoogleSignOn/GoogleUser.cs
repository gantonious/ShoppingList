using System.Security.Claims;
using Google.Apis.Auth;

namespace ShoppingList.WebServices.GoogleSignOn
{
    public class GoogleUser : ClaimsPrincipal
    {
        public GoogleJsonWebSignature.Payload UserPayload { get; set; }
    }
}