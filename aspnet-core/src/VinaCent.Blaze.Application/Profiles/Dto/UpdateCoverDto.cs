using Microsoft.AspNetCore.Http;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class UpdateCoverDto
    {
        public string Cover { get; set; }
        public IFormFile File { get; set; }
    }
}
