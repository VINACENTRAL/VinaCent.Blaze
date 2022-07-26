using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Configuration.Dto
{
    public class ChangeUiThemeModeInput
    {
        [Required]
        [StringLength(10)]
        public string ThemeMode { get; set; }
    }
}
