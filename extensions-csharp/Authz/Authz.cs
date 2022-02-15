using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace extensions_csharp.Authz
{
    public class LockerOwnerRequirement : IAuthorizationRequirement
    {
        public Lazy<Task<string>> FetchLockerOwnerId = new();
    }

    // this gets loaded automagically with RegisterAssemblyPublicNonGenericClasses... but it can't be a nested class
    [UsedImplicitly]
    public class LockerOwnerHandler : AuthorizationHandler<LockerOwnerRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            LockerOwnerRequirement requirement)
        {
            var userId = context.User.FindFirst(Authz.IdentityClaimType)?.Value;
            if (userId != null)
            {
                var lockerOwnerId = await requirement.FetchLockerOwnerId.Value;
                if (lockerOwnerId == userId)
                    context.Succeed(requirement);
            }
        }
    }

    public class NoPeonsAllowedRequirement : IAuthorizationRequirement
    {
    }

    [UsedImplicitly]
    public class NoPeonsAllowedHandler : AuthorizationHandler<NoPeonsAllowedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            NoPeonsAllowedRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, "Peon"))
                context.Fail();
            else
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    // Don't want to use 'Authorization' namespace... will collide badly
    public static class Authz
    {
        private const string IdentityIssuer = "some-identity-issuer";
        internal const string IdentityClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";


        public static async Task Demo()
        {
            Console.WriteLine("Authorization / Authz =================================");

            var boss = CreatePrincipal(IdentityIssuer, "Boss");
            var peon = CreatePrincipal(IdentityIssuer, "Peon");
            var outsider = CreatePrincipal("another-issuer", "Boss");

            var bossesOnly = new AuthorizationPolicyBuilder()
                .RequireClaim("iss", IdentityIssuer)
                .RequireRole("Boss")
                .Build();
            var authz = CreateAuthorizationService(opts => opts.AddPolicy(nameof(bossesOnly), bossesOnly));

            ////////////////////////// Using a simple named policy
            async Task CheckSimple(ClaimsPrincipal principal, bool expectSuccess)
            {
                var result = await authz!.AuthorizeAsync(principal, nameof(bossesOnly));
                Debug.Assert(result.Succeeded == expectSuccess);
            }

            await CheckSimple(boss, true);
            await CheckSimple(peon, false);
            await CheckSimple(outsider, false);

            ///////////////////////////// using a custom requirement
            // some expensive command that uses the supplied 'lockerId' parameter.  The result can be used again later
            var fetchLockerOwnerId1 = new Lazy<Task<string>>(() => Task.FromResult("user-1"));
            var fetchLockerOwnerId2 = new Lazy<Task<string>>(() => Task.FromResult("user-2"));

            async Task CheckCustom(ClaimsPrincipal principal, Lazy<Task<string>> fetchLockerOwner, bool expectSuccess)
            {
                var requirements = new[]
                {
                    new LockerOwnerRequirement
                    {
                        FetchLockerOwnerId = fetchLockerOwner
                    }
                };
                var result = await authz!.AuthorizeAsync(principal, null, requirements);
                Debug.Assert(result.Succeeded == expectSuccess);
            }

            await CheckCustom(boss, fetchLockerOwnerId1, true);
            await CheckCustom(peon, fetchLockerOwnerId1, true); // no role/issuer checks yet
            await CheckCustom(outsider, fetchLockerOwnerId1, true); // no role/issuer checks yet
            await CheckCustom(boss, fetchLockerOwnerId2, false);

            /////////////////////// using a combination of requirements (most flexible)
            async Task CheckCombo(ClaimsPrincipal principal, Lazy<Task<string>> fetchLockerOwner,
                params Type[] failedRequirements)
            {
                var result = await authz!.AuthorizeAsync(principal, new AuthorizationPolicyBuilder()
                    .RequireClaim("iss", IdentityIssuer)
                    .RequireRole("Boss")
                    .AddRequirements(new LockerOwnerRequirement
                    {
                        FetchLockerOwnerId = fetchLockerOwner
                    })
                    .Build());
                Debug.Assert(result.Succeeded != failedRequirements.Any());
                foreach (var requirement in failedRequirements)
                {
                    Debug.Assert(result.Failure!.FailedRequirements.Any(r => r.GetType() == requirement));
                }
            }

            await CheckCombo(boss, fetchLockerOwnerId1);
            await CheckCombo(peon, fetchLockerOwnerId1, typeof(RolesAuthorizationRequirement));
            await CheckCombo(outsider, fetchLockerOwnerId1, typeof(ClaimsAuthorizationRequirement));
            await CheckCombo(boss, fetchLockerOwnerId2, typeof(LockerOwnerRequirement));

            //////////////////////// confirm how requirements relate
            // NOTE: All requirements must be satisfied... but might have multiple handlers, only one of which needs to 'succeed'
            // var x = await authz.AuthorizeAsync(peon, null, new AuthorizationPolicyBuilder()
            //     .Combine(bossesOnly)
            //     .Build());
            // Debug.Assert(x.Succeeded);

            /////////////////////// Explicit fail
            var explicitResult = await authz.AuthorizeAsync(peon, null!, new NoPeonsAllowedRequirement());
            Debug.Assert(!explicitResult.Succeeded);
            Debug.Assert(explicitResult.Failure!.FailCalled);
            // Explicit fail seems to make t his collection empty
            // Debug.Assert(result.Failure!.FailedRequirements.Any(r => r is NoPeonsAllowedRequirement));
            Debug.Assert(!explicitResult.Failure!.FailedRequirements.Any());

            ////////////////////// Each requirement must succeed
            var impossibleRequirements = new IAuthorizationRequirement[]
            {
                new LockerOwnerRequirement {FetchLockerOwnerId = fetchLockerOwnerId1},
                new LockerOwnerRequirement {FetchLockerOwnerId = fetchLockerOwnerId2}
            };
            var multiResult = await authz.AuthorizeAsync(peon, null!, impossibleRequirements);
            Debug.Assert(!multiResult.Succeeded);

            multiResult = await authz.AuthorizeAsync(peon, null!, new AuthorizationPolicyBuilder()
                .AddRequirements(impossibleRequirements)
                .Build());
            Debug.Assert(!multiResult.Succeeded);

            multiResult = await authz.AuthorizeAsync(peon, null!, new AuthorizationPolicyBuilder()
                .AddRequirements(impossibleRequirements[0])
                .AddRequirements(impossibleRequirements[1])
                .Build());
            Debug.Assert(!multiResult.Succeeded);

            multiResult = await authz.AuthorizeAsync(peon, null!, new AuthorizationPolicyBuilder()
                .AddRequirements(impossibleRequirements[0])
                .Build());
            Debug.Assert(multiResult.Succeeded); // sanity check
        }

        private static ClaimsPrincipal CreatePrincipal(string identityIssuer, params string[] roles)
        {
            var claims = roles.Select(r => new Claim(ClaimTypes.Role, r))
                .Append(new Claim(IdentityClaimType, "user-1"))
                .Append(new Claim("iss", identityIssuer));

            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }

        private static IAuthorizationService CreateAuthorizationService(Action<AuthorizationOptions> configureAuthorization)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddAuthorizationCore(configureAuthorization);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(Authz)))
                .Where(c => c.GetInterface(nameof(IAuthorizationHandler)) != null)
                .AsPublicImplementedInterfaces();
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IAuthorizationService>();
        }
    }
}
