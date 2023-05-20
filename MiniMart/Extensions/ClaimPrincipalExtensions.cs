using System.Security.Claims;

namespace MiniMart.API.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            if (!int.TryParse(user.FindFirst((Claim x) => x.Type == "_user_id")?.Value, out var result))
            {
                return 0;
            }

            return result;
        }

        public static bool HasPermission(this ClaimsPrincipal user, string permission)
        {
            return user?.FindFirst((Claim c) => c.Type == "permission" && (c.Value?.Equals(permission, StringComparison.InvariantCultureIgnoreCase) ?? false)) != null;
        }

        public static bool HasRoles(this ClaimsPrincipal user, string role)
        {
            return user?.FindFirst((Claim c) => c.Type == "_roles" && (c.Value?.Equals(role, StringComparison.InvariantCultureIgnoreCase) ?? false)) != null;
        }

        public static bool HasAnyPermission(this ClaimsPrincipal user, params string[] permissions)
        {
            if (permissions == null)
            {
                return false;
            }

            return user?.FindFirst((Claim c) => c.Type == "permission" && permissions.Contains<string>(c.Value, StringComparer.InvariantCultureIgnoreCase)) != null;
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user?.FindFirst((Claim x) => x.Type == "_name")?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user?.FindFirst((Claim x) => x.Type == "_email")?.Value;
        }
    }
}
