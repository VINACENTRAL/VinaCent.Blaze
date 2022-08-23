using VinaCent.Blaze.DataAnnotations;
using VinaCent.Blaze.Validation;

namespace VinaCent.Blaze.Profiles.Dto
{
    /// <summary>
    /// Liên kết mạng xã hội
    /// </summary>
    public class SocialNetwork
    {
        /// <summary>
        /// Icon
        /// </summary>
        [AppRequired]
        public string SocialIcon { get; set; }

        /// <summary>
        /// Tên tài khoản
        /// </summary>
        [AppRequired]
        public string SocialName { get; set; }

        /// <summary>
        /// Đường dẫn liên kết
        /// </summary>
        [AppRequired]
        [AppRegex(ValidationHelper.UrlWithHttpRegex)]
        public string SocialUrl { get; set; }
    }
}
