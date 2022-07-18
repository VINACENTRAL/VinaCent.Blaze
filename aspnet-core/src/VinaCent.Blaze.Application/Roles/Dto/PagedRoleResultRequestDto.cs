using Abp.Application.Services.Dto;

namespace VinaCent.Blaze.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

