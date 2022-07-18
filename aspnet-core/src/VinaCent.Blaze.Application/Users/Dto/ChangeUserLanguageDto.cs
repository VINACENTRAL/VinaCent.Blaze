using System.ComponentModel.DataAnnotations;

namespace VinaCent.Blaze.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}