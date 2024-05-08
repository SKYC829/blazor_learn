using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace BlazorLearn_WebApp.Client.Components.L14.Authorizes
{
    public class AdultAuthorizationRequirement : AuthorizationHandler<AdultAuthorizationRequirement>, IAuthorizationRequirement
    {
        private readonly int _age;
        public int Age => _age;

        public AdultAuthorizationRequirement (int age)
        {
            _age = Math.Max (age, 0);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdultAuthorizationRequirement requirement)
        {
            if(context.User is not null && context.User.HasClaim(p=>p.Type.ToLower() == "age"))
            {
                string value = context.User.FindFirst("age")!.Value;
                if(int.TryParse(value,out int userAge) && userAge >= _age)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail(new AuthorizationFailureReason(this, $"用户年龄必须大于{Age}岁"));
                }
            }
            return Task.CompletedTask;
        }
    }
}
