using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Configuration.Dto
{
    public class ChangeUiThemeModeInput
    {
        [AppRequired]
        [AppStringLength(10)]
        public string ThemeMode { get; set; }
    }
}
