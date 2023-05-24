using System.Security.Claims;
using Utils.CrossCuttingConcerns.Constants;

namespace Utils.WebAPI.Extensions
{
    public static class HttpExtension
    {
        /// <summary>
        /// Check if the request is for swagger.
        /// </summary>
        public static bool HasSwaggerRequest(this HttpContext context)
        {
            return context != null && context.Request.Path.Value.Contains(ConfigurationConstant.Swagger, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Get the time zone that was sent along in the Header.
        /// </summary>
        public static string GetTimeZone(this HttpContext context) => context.Request.Headers[ConfigurationConstant.TimeZoneKey].FirstOrDefault();

        public static Guid? GetUserId(this HttpContext context)
        {
            return Guid.TryParse(context.GetIdentityValueByTypeName(ClaimTypes.Name), out var userId) ? userId : null;
        }

        public static string GetRole(this HttpContext context)
        {
            return context.GetIdentityValueByTypeName(ClaimTypes.Role);
        }

        public static string GetAuthorizationToken(this HttpContext context)
        {
            return context.Request.Headers[ConfigurationConstant.AuthorizationHeader].FirstOrDefault();
        }

        public static string GetIdentityValueByTypeName(this HttpContext context, string typeName)
        {
            var userClaimsIdentity = context?.User.Identities.FirstOrDefault();

            return userClaimsIdentity?.FindFirst(typeName)?.Value;
        }
    }
}
