namespace FA.JustBlog.Core.Models
{
    public class Tag
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public virtual IList<PostTagMap> PostTags { get; set; }
    }
}
