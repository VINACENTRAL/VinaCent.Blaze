using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VinaCent.Blaze.Helpers
{
    public static class StringHelper
    {
        public const string FileSeparator = "/";
        /// <summary>
        /// Prevent have path separator char '\' in windows
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string TrueCombine(params string[] paths)
        {
            if (paths.Length <= 1) return Path.Combine(paths).Replace("\\", FileSeparator);
            
            for (var i = 1; i < paths.Length; i++)
            {
                paths[i] = paths[i].TrimStart(FileSeparator.First()).TrimStart('\\');
            }

            return Path.Combine(paths).Replace("\\", FileSeparator);
        }

        public static string TrueCombieAndEnsureDirExist(params string[] paths)
        {
            var path = TrueCombine(paths);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path!);
            }
            return path;
        }

        public static string OSCombine(params string[] paths)
        {
            if (paths.Length > 1)
            {
                for (var i = 1; i < paths.Length; i++)
                {
                    paths[i] = paths[i].TrimStart(FileSeparator.First()).TrimStart('\\');
                }
            }

            return Path.Combine(paths);
        }

        public static string UppercaseFirstChar(this string input)
        {
            return input[..1].ToUpper() + input[1..].ToLower();
        }

        public static string EncodePath(this string path)
        {
            var input = Encoding.UTF8.GetBytes(path);
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length < 1)
            {
                return string.Empty;
            }

            int endPos;

            ////////////////////////////////////////////////////////
            // Step 1: Do a Base64 encoding
            var base64Str = Convert.ToBase64String(input);

            ////////////////////////////////////////////////////////
            // Step 2: Find how many padding chars are present in the end
            for (endPos = base64Str.Length; endPos > 0; endPos--)
            {
                if (base64Str[endPos - 1] != '=') // Found a non-padding char!
                {
                    break; // Stop here
                }
            }

            ////////////////////////////////////////////////////////
            // Step 3: Create char array to store all non-padding chars,
            //      plus a char to indicate how many padding chars are needed
            var base64Chars = new char[endPos + 1];
            base64Chars[endPos] =
                (char) ('0' + base64Str.Length -
                        endPos); // Store a char at the end, to indicate how many padding chars are needed

            ////////////////////////////////////////////////////////
            // Step 3: Copy in the other chars. Transform the "+" to "-", and "/" to "_"
            for (var iter = 0; iter < endPos; iter++)
            {
                var c = base64Str[iter];

                base64Chars[iter] = c switch
                {
                    '+' => '-',
                    '/' => '_',
                    '=' => c,
                    _ => c
                };
            }

            return new string(base64Chars);
        }

        public static string MaskHiddingEmailAddress(this string email)
        {
            string pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(email, pattern, m => new string('*', m.Length));
            return result;
        }
    }
}