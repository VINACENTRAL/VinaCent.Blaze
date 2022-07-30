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

        public string Picture { get; set; } = "https://upload-os-bbs.hoyolab.com/upload/2021/09/08/10805287/d6cac34c75cd7ccb346b1133cff2e192_5253703590737420027.jpg?x-oss-process=image/resize,s_600/quality,q_80/auto-orient,0/interlace,1/format,jpg";

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
