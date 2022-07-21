using Abp.Dependency;
using VinaCent.Blaze.Configuration.Ui;
using VinaCent.Blaze.Themes;

namespace VinaCent.Blaze.Web.Themes.Velzon
{
    public class VelzonTheme : ITheme, ITransientDependency
    {
        public const string Name = "Velzon";
        public const string Application = "~/Themes/Velzon/Layouts/Application.cshtml";
        public const string Empty = "~/Themes/Velzon/Layouts/Empty.cshtml";
        public const string Clearer = "~/Themes/Velzon/Layouts/Clearer.cshtml";

        public string GetLayout(string name, bool fallbackToDefault = true)
        {
            return name switch
            {
                StandardLayouts.Application => Application,
                StandardLayouts.Account => null,
                StandardLayouts.Empty => Empty,
                _ => fallbackToDefault ? Application : null,
            };
        }
    }
}
