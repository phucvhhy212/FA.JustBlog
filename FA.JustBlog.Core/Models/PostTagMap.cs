namespace FA.JustBlog.Core.Models
{
    public class PostTagMap
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
