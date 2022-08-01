using Abp.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VinaCent.Blaze.Authorization.Claims;

namespace VinaCent.Blaze.Authorization.CurrentUser
{
    public static class CurrentUserExtensions
    {
        [CanBeNull]
        public static string FindClaimValue(this ICurrentUser currentUser, string claimType)
        {
            return currentUser.FindClaim(claimType)?.Value;
        }

        public static T FindClaimValue<T>(this ICurrentUser currentUser, string claimType)
            where T : struct
        {
            var value = currentUser.FindClaimValue(claimType);
            if (value == null)
            {
                return default;
            }

            return value.To<T>();
        }

        public static long GetId(this ICurrentUser currentUser)
        {
            Debug.Assert(currentUser.Id != null, "currentUser.Id != null");

            return currentUser.Id.Value;
        }

        public static long? FindImpersonatorTenantId([NotNull] this ICurrentUser currentUser)
        {
            var impersonatorTenantId = currentUser.FindClaimValue(AbpClaimTypes.ImpersonatorTenantId);
            if (impersonatorTenantId.IsNullOrWhiteSpace())
            {
                return null;
            }
            if (long.TryParse(impersonatorTenantId, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static long? FindImpersonatorUserId([NotNull] this ICurrentUser currentUser)
        {
            var impersonatorUserId = currentUser.FindClaimValue(AbpClaimTypes.ImpersonatorUserId);
            if (impersonatorUserId.IsNullOrWhiteSpace())
            {
                return null;
            }
            if (long.TryParse(impersonatorUserId, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static string FindImpersonatorTenantName([NotNull] this ICurrentUser currentUser)
        {
            return currentUser.FindClaimValue(AbpClaimTypes.ImpersonatorTenantName);
        }

        public static string FindImpersonatorUserName([NotNull] this ICurrentUser currentUser)
        {
            return currentUser.FindClaimValue(AbpClaimTypes.ImpersonatorUserName);
        }
    }
}
