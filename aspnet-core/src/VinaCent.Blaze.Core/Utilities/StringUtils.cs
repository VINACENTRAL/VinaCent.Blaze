using Abp.Extensions;
using System.Text.RegularExpressions;

namespace VinaCent.Blaze.Utilities
{
    public static class StringUtils
    {
        private static readonly string[] VietnameseSigns = new string[] {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string RemoveVietnameseAccent(this string str)
        {
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        public static string Remove(this string input, params string[] defacts)
        {
            foreach (var defact in defacts)
            {
                input = input.Replace(defact, "");
            }
            return input;
        }

        public static string ConcatToUri(this string input, params string[] paths)
        {
            var result = input.Trim().Trim('/');
            foreach (var path in paths)
            {
                if (result.Contains('?') || path.StartsWith("?"))
                {
                    if (path.StartsWith("?") && !result.Contains('?'))
                    {
                        result += path;
                    }
                    else
                    {
                        result += path.TrimStart('?').Replace("?", "%3F").EnsureStartsWith('&');
                    }
                }
                else
                {
                    result += "/" + path.Trim().Trim('/').Replace("//", "/");
                }
            }
            return result;
        }

        public static string GenerateSlug(this string phrase, int maxLength = -1)
        {
            phrase = phrase.RemoveVietnameseAccent().RemoveAccent().ToLower();
            // invalid chars           
            phrase = Regex.Replace(phrase, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            phrase = Regex.Replace(phrase, @"\s+", " ").Trim();
            // cut and trim 
            if (maxLength > 0)
            {
                phrase = phrase.Substring(0, phrase.Length <= maxLength ? phrase.Length : maxLength).Trim();
            }
            phrase = Regex.Replace(phrase, @"\s", "-"); // hyphens   
            return phrase;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
