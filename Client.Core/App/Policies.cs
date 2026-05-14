using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Client.Core.App
{
    public static class Policies
    {
        public const string EducatorOnly = "Educator";
        public const string AdminOnly = "Admin";
        public const string StudentOnly = "Student";

        public static void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(EducatorOnly, policy =>
                policy.RequireClaim(ClaimTypes.Role, "1"));

            options.AddPolicy(StudentOnly, policy =>
                policy.RequireClaim(ClaimTypes.Role, "2"));

            options.AddPolicy(AdminOnly, policy =>
                policy.RequireClaim(ClaimTypes.Role, "3"));
        }
    }
}