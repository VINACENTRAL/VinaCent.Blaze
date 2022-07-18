using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Configuration.Dto
{
    public class ChangeUiThemeInput
    {
        [AppRequired]
        [AppStringLength(32)]
        public string Theme { get; set; }
    }
}
