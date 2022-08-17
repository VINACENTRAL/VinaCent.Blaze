using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;
using System.Globalization;
using VinaCent.Blaze.Authorization.Users;

namespace VinaCent.Blaze.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Avatar { get; set; } 
        public string[] Roles { get; set; }

        public virtual string FullName
        {
            get
            {
                string fullName;
                var styleName_SureName_Name = new List<string> { "vi", "vi-VN" };

                if (styleName_SureName_Name.Contains(CultureInfo.CurrentUICulture.Name))
                {
                    fullName = Surname + " " + Name;
                }
                else
                {
                    fullName = Name + " " + Surname;
                }

                fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fullName.Trim());

                return fullName;
            }
        }
    }
}
