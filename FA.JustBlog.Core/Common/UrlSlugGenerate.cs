using System.Text;
using System.Text.RegularExpressions;

namespace FA.JustBlog.Core.Common
{
    public static class UrlSlugGenerate
    {
        public static string GenerateSlug(string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // Remove all non-alphanumeric characters except for spaces and hyphens
            str = Regex.Replace(str, @"\s+", " ").Trim(); // Replace multiple spaces with a single space
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // Trim the string to 45 characters
            str = Regex.Replace(str, @"\s", "-"); // Replace spaces with hyphens
            return str;
        }

        private static string RemoveAccent(string txt)
        {
            byte[] bytes = Encoding.GetEncoding(0).GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
