using AutoMapper;
using System;
using System.Globalization;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.Profiles.Dto
{
    public class UserProfileMapProfile : Profile
    {
        public UserProfileMapProfile()
        {
            var pattern = CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern;

            CreateMap<User, ProfileDto>()
            .ForMember(dest => dest.HasPassword,
                op => op.MapFrom(src => src.Password != null));
        }
    }
}
