using Abp;
using Abp.Configuration;
using Abp.Extensions;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;

namespace VinaCent.Blaze.Configuration
{
    public static class SettingManagerExtensions
    {
        public static async Task<bool> IsTrueAsync([NotNull] this ISettingManager settingManager, [NotNull] string name)
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            return string.Equals(
                await settingManager.GetSettingValueAsync(name),
                "true",
                StringComparison.OrdinalIgnoreCase
            );
        }

        public static async Task<T> GetAsync<T>([NotNull] this SettingManager settingManager, [NotNull] string name, T defaultValue = default)
            where T : struct
        {
            Check.NotNull(settingManager, nameof(settingManager));
            Check.NotNull(name, nameof(name));

            var value = await settingManager.GetSettingValueAsync(name);
            return value?.To<T>() ?? defaultValue;
        }
    }
}
