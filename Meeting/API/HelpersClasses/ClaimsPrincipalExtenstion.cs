using System.Security.Claims;

namespace API.HelpersClasses
{
    internal static class ClaimsPrincipalExtenstion
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            string? userName = user.FindFirst(ClaimTypes.Name)?.Value;
            if (userName != null)
                return userName;
            else
                return "";

        }

        public static int GetUserId(this ClaimsPrincipal user)
        {

            string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
                return int.Parse(userId);
            else
                return 0;
        }

        public static List<string> GetUserRole(this ClaimsPrincipal user)
        {
            List<string>? userRole = user.FindAll(ClaimTypes.Role)?.ToList().Select(x => x.Value).ToList();
            if (userRole != null)
                return userRole;
            else
                return new List<string>();
        }
    }
}
