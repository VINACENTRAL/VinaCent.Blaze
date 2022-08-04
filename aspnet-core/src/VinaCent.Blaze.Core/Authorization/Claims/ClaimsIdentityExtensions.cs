using Abp;
using Abp.Extensions;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace VinaCent.Blaze.Authorization.Claims
{
    public static class ClaimsIdentityExtensions
    {
        public static long? FindUserId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var userIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(userIdOrNull.Value, out long guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindUserId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var userIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.UserId);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(userIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindTenantId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var tenantIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
            if (tenantIdOrNull == null || tenantIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(tenantIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindTenantId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var tenantIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
            if (tenantIdOrNull == null || tenantIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(tenantIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static string FindClientId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var clientIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.ClientId);
            if (clientIdOrNull == null || clientIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            return clientIdOrNull.Value;
        }

        public static string FindClientId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var clientIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.ClientId);
            if (clientIdOrNull == null || clientIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            return clientIdOrNull.Value;
        }

        public static long? FindEditionId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var editionIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.EditionId);
            if (editionIdOrNull == null || editionIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(editionIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindEditionId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var editionIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.EditionId);
            if (editionIdOrNull == null || editionIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(editionIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindImpersonatorTenantId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var impersonatorTenantIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
            if (impersonatorTenantIdOrNull == null || impersonatorTenantIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(impersonatorTenantIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindImpersonatorTenantId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var impersonatorTenantIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
            if (impersonatorTenantIdOrNull == null || impersonatorTenantIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(impersonatorTenantIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindImpersonatorUserId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var impersonatorUserIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);
            if (impersonatorUserIdOrNull == null || impersonatorUserIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(impersonatorUserIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindImpersonatorUserId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var impersonatorUserIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);
            if (impersonatorUserIdOrNull == null || impersonatorUserIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (long.TryParse(impersonatorUserIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static ClaimsIdentity AddIfNotContains(this ClaimsIdentity claimsIdentity, Claim claim)
        {
            Check.NotNull(claimsIdentity, nameof(claimsIdentity));

            if (!claimsIdentity.Claims.Any(x => string.Equals(x.Type, claim.Type, StringComparison.OrdinalIgnoreCase)))
            {
                claimsIdentity.AddClaim(claim);
            }

            return claimsIdentity;
        }

        public static ClaimsIdentity AddOrReplace(this ClaimsIdentity claimsIdentity, Claim claim)
        {
            Check.NotNull(claimsIdentity, nameof(claimsIdentity));

            foreach (var x in claimsIdentity.FindAll(claim.Type).ToList())
            {
                claimsIdentity.RemoveClaim(x);
            }

            claimsIdentity.AddClaim(claim);

            return claimsIdentity;
        }

        public static ClaimsPrincipal AddIdentityIfNotContains([NotNull] this ClaimsPrincipal principal, ClaimsIdentity identity)
        {
            Check.NotNull(principal, nameof(principal));

            if (!principal.Identities.Any(x => string.Equals(x.AuthenticationType, identity.AuthenticationType, StringComparison.OrdinalIgnoreCase)))
            {
                principal.AddIdentity(identity);
            }

            return principal;
        }

    }
}
