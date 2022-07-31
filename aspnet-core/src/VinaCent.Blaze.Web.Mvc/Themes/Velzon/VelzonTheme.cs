using Abp.Dependency;
using VinaCent.Blaze.Themes;

namespace VinaCent.Blaze.Web.Themes.Velzon
{
    public class VelzonTheme : ITheme, ITransientDependency
    {
        public const string Name = "Velzon";
        public const string Application = "~/Themes/Velzon/Layouts/Application.cshtml";
        public const string Account = "~/Themes/Velzon/Layouts/Account.cshtml";
        public const string Error = "~/Themes/Velzon/Layouts/ErrorLayout.cshtml";
        public const string Empty = "~/Themes/Velzon/Layouts/Empty.cshtml";
        public const string Clearer = "~/Themes/Velzon/Layouts/Clearer.cshtml";

        public string GetLayout(string name, bool fallbackToDefault = true)
        {
            return name switch
            {
                StandardLayouts.Application => Application,
                StandardLayouts.Account => Account,
                StandardLayouts.Empty => Empty,
                StandardLayouts.Error => Error,
                _ => fallbackToDefault ? Application : null,
            };
        }
    }
}
