using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace extensions_csharp.Authz
{
    // Don't want to use 'Authorization' namespace... will collide badly
    public static class Authz
    {
        public static async Task Demo()
        {
            Console.WriteLine("Authorization / Authz =================================");

            var identityIssuer = "some-identity-issuer";
            var boss = CreatePrincipal(identityIssuer, "Boss");
            var peon = CreatePrincipal(identityIssuer, "Peon");
            var outsider = CreatePrincipal("another-issuer", "Boss");

            var bossesOnly = new AuthorizationPolicyBuilder()
                .RequireClaim("iss", identityIssuer)
                .RequireRole("Boss")
                .Build();
            var authz = CreateAuthorizationService(opts => opts.AddPolicy(nameof(bossesOnly), bossesOnly));

            async Task Check(ClaimsPrincipal principal, bool expectSuccess)
            {
                var result = await authz!.AuthorizeAsync(principal, nameof(bossesOnly));
                Debug.Assert(result.Succeeded == expectSuccess);
            }

            await Check(boss, true);
            await Check(peon, false);
            await Check(outsider, false);
        }

        private static ClaimsPrincipal CreatePrincipal(string identityIssuer, params string[] roles)
        {
            var claims = roles.Select(r => new Claim(ClaimTypes.Role, r))
                .Append(new Claim("iss", identityIssuer));

            return new ClaimsPrincipal(new ClaimsIdentity(claims));
        }

        private static IAuthorizationService CreateAuthorizationService(Action<AuthorizationOptions> configureAuthorization)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddAuthorizationCore(configureAuthorization);
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IAuthorizationService>();
        }
    }
}
