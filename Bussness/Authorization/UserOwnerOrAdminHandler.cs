using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


public class UserOwnerOrAdminHandler : AuthorizationHandler<UserOwnerOrAdminRequirement, int>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserOwnerOrAdminRequirement requirement, int targetUserId)
    {
    
        if (context.User.IsInRole("admin") || context.User.IsInRole("superAdmin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdClaim, out int authenticatedUserId) && authenticatedUserId == targetUserId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}