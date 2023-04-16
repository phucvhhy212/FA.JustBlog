
namespace FA.JustBlog.Core.Models
{
    public class Post
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string PostContent { get; set; }
        public string UrlSlug { get; set; }

        public bool Published { get; set; }

        public DateTime PostedOn { get; set; }

        public bool Modified { get; set; }
        public int CategoryId { get; set; }
        

        public int ViewCount { get; set; }

        public int RateCount { get; set; }

        public int TotalRate { get; set; }

        public decimal Rate { get; set; }

        public virtual IList<Comment> Comments { get; set; }
        public Category Category { get; set; }

        public virtual IList<PostTagMap> PostTags { get; set; }

    }
}
