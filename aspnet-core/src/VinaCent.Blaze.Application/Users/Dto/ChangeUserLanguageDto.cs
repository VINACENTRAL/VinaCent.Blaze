using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [AppRequired]
        public string LanguageName { get; set; }
    }
}