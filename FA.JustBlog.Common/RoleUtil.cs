namespace FA.JustBlog.Common
{
    public static class RoleUtils
    {
        public const string BLOG_OWER = "Blog Owner";
        public const string USER = "User";
        public const string CONTRIBUTOR = "Contributor";
        public const string OTHER = "Other";

        public static List<string> GetAdminRoles()
        {
            return new List<string>() { BLOG_OWER, CONTRIBUTOR };
        }
    }
}