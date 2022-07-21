using Abp.Extensions;
using System.Text.RegularExpressions;

namespace VinaCent.Blaze.Utilities
{
    public static class StringUtils
    {
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

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
