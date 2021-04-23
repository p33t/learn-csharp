﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace extensions_csharp.Authz
{
    public class LockerOwnerRequirement : IAuthorizationRequirement
    {
        public Lazy<Task<string>> FetchLockerOwnerId { get; init; } = new();
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

            async Task CheckSimple(ClaimsPrincipal principal, bool expectSuccess)
            {
                var result = await authz!.AuthorizeAsync(principal, nameof(bossesOnly));
                Debug.Assert(result.Succeeded == expectSuccess);
            }

            await CheckSimple(boss, true);
            await CheckSimple(peon, false);
            await CheckSimple(outsider, false);

            // some expensive command that uses the supplied 'lockerId' parameter.  The result can be used again later
            var fetchLockerOwnerId1 = new Lazy<Task<string>>(() => Task.FromResult("user-1"));
            var fetchLockerOwnerId2 = new Lazy<Task<string>>(() => Task.FromResult("user-2"));

            async Task CheckCustom(ClaimsPrincipal principal, Lazy<Task<string>> fetchLockerOwner, bool expectSuccess)
            {
                var requirements = new []
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
            await CheckCustom(boss, fetchLockerOwnerId2, false);
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
