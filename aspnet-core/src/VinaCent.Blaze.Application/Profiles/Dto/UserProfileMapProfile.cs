using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class UserProfileMapProfile : Profile
    {
        public UserProfileMapProfile()
        {
            CreateMap<User, ProfileDto>()
            .ForMember(dest => dest.HasPassword,
                op => op.MapFrom(src => src.Password != null));
        }
    }
}
