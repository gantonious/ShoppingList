using ShoppingList.WebServices.GoogleSignOn;

namespace Microsoft.AspNetCore.Builder
{
    public static class GoogleSignOnExtensions
    {
        public static IApplicationBuilder UseGoogleSignOn(this IApplicationBuilder applicationBuilder, GoogleSignOnOptions options)
        {
            return applicationBuilder.UseMiddleware<GoogleSignOnMiddleware>(options);
        }
    }
}