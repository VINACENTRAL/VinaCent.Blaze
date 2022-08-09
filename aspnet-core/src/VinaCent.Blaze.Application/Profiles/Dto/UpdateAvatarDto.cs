using Microsoft.AspNetCore.Http;

namespace VinaCent.Blaze.Profiles.Dto;

public class UpdateAvatarDto
{
    public string Avatar { get; set; }
    public IFormFile File { get; set; }
}