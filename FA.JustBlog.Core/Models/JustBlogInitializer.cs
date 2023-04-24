using FA.JustBlog.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Models
{
    public static class JustBlogInitializer
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Technology",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Technology"),
                    Description = "Articles related to technology."
                },
                new Category
                {
                    Id = 2,
                    Name = "Sports",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Sports"),
                    Description = "Articles related to sports."
                },new Category
                {
                    Id = 3,
                    Name = "Entertainment",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Entertainment"),
                    Description = "Articles related to entertainment."
                }
            });

            modelBuilder.Entity<Post>().HasData(new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Title = "Introduction to C#",
                    ShortDescription = "An introduction to C# programming language.",
                    PostContent = "C# is a modern object-oriented programming language...",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Introduction to C#"),
                    Published = true,
                    PostedOn = new DateTime(2022, 1, 1),
                    Modified = false,
                    CategoryId = 1,
                    ViewCount = 100,
                    RateCount = 90,
                    TotalRate = 100,
                    
                },
                new Post
                {
                    Id = 2,
                    Title = "The Benefits of Regular Exercise",
                    ShortDescription = "Why regular exercise is important for your health.",
                    PostContent = "Regular exercise has many benefits...",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("The Benefits of Regular Exercise"),
                    Published = true,
                    PostedOn = new DateTime(2022, 1, 2),
                    Modified = false,
                    CategoryId = 2,
                    ViewCount = 120,
                    RateCount = 90,
                    TotalRate = 100,
                    

                },new Post
                {
                    Id = 3,
                    Title = "The Evolution of Basketball",
                    ShortDescription = "How basketball has changed over the years.",
                    PostContent = "Basketball has undergone many changes...",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("The Evolution of Basketball"),
                    Published = false,
                    PostedOn = new DateTime(2022, 1, 3),
                    Modified = false,
                    CategoryId = 3,
                    ViewCount = 120,
                    RateCount = 80,
                    TotalRate = 100,
                    

                },
                new Post
                {
                    Id = 4,
                    Title = "Introduction to Programming",
                    ShortDescription = "An overview of programming concepts",
                    PostContent = "Programming is the process of designing, writing, testing, and maintaining software. It involves creating instructions that a computer can execute to solve a particular problem or perform a specific task. Programming languages, such as Java and Python, are used to write these instructions, which are then compiled or interpreted into machine code that a computer can understand.",
                    UrlSlug = "introduction-to-programming",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = false,
                    CategoryId = 1,
                    ViewCount = 10,
                    RateCount = 2,
                    TotalRate = 8
                }
                ,new Post
                {
                    Id = 5,
                    Title = "The Benefits of Exercise",
                    ShortDescription = "Why regular exercise is important for health",
                    PostContent = "Regular exercise has numerous health benefits. It can help maintain a healthy weight, reduce the risk of chronic diseases such as diabetes and heart disease, improve mental health, and increase overall well-being. Exercise can also improve sleep, increase energy levels, and reduce stress and anxiety. The recommended amount of exercise varies depending on age and fitness level, but generally adults should aim for at least 150 minutes of moderate-intensity aerobic exercise per week.",
                    UrlSlug = "benefits-of-exercise",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = false,
                    CategoryId = 2,
                    ViewCount = 15,
                    RateCount = 4,
                    TotalRate = 16
                },
                new Post
                {
                    Id = 6,
                    Title = "The Importance of Time Management",
                    ShortDescription = "Tips for managing your time effectively",
                    PostContent = "Effective time management is essential for productivity and success. It involves prioritizing tasks, setting goals, and planning ahead. One helpful technique is the Pomodoro method, which involves breaking work into 25-minute intervals with short breaks in between. Another strategy is to use a task list or calendar to stay organized and track progress. Time management can also involve delegating tasks, saying no to distractions, and taking breaks to recharge.",
                    UrlSlug = "importance-of-time-management",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = false,
                    CategoryId = 3,
                    ViewCount = 20,
                    RateCount = 3,
                    TotalRate = 12
                },
                new Post
                {
                    Id = 7,
                    Title = "The Benefits of Yoga",
                    ShortDescription = "How practicing yoga can improve your health",
                    PostContent = "Yoga is a mind-body practice that combines physical postures, breathing exercises, and meditation or relaxation. It has been shown to have numerous health benefits, including reducing stress and anxiety, improving flexibility and balance, and promoting relaxation and better sleep. Yoga may also help reduce inflammation and lower blood pressure and cholesterol levels. There are many different styles of yoga, so it is important to find a practice that works for you. Practicing yoga regularly can be a great addition to a healthy lifestyle.",
                    UrlSlug = "benefits-of-yoga",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = false,
                    CategoryId = 1,
                    ViewCount = 50,
                    RateCount = 12,
                    TotalRate = 48
                },new Post
                {
                    Id = 8,
                    Title = "The Power of Positive Thinking",
                    ShortDescription = "How changing your mindset can change your life",
                    PostContent = "Positive thinking is a mental attitude that focuses on the good in any situation and expects positive outcomes. It has been shown to have numerous benefits, including reducing stress and anxiety, improving overall health and well-being, and increasing resilience and optimism. Positive thinking can help people cope with difficult situations and overcome challenges. It is not about ignoring negative experiences or denying reality, but rather about approaching them with a positive and proactive mindset. Developing a practice of positive thinking can lead to greater happiness and success in all areas of life.",
                    UrlSlug = "power-of-positive-thinking",
                    Published = true,
                    PostedOn = DateTime.Now,
                    Modified = false,
                    CategoryId = 2,
                    ViewCount = 55,
                    RateCount = 14,
                    TotalRate = 56
                }
            });

            modelBuilder.Entity<Tag>().HasData(new List<Tag>
            {
                new Tag
                {
                    Id = 1,
                    Name = "Programming",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Programming"),
                    Description = "Articles related to programming.",
                    Count = 10
                },
                new Tag
                {
                    Id = 2,
                    Name = "Web Development",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Web Development"),
                    Description = "Articles related to web development.",
                    Count = 5
                },new Tag
                {
                    Id = 3,
                    Name = "Data Science",
                    UrlSlug = UrlSlugGenerate.GenerateSlug("Data Science"),
                    Description = "Articles related to data science.",
                    Count = 3
                }
            });

            modelBuilder.Entity<PostTagMap>().HasData(new List<PostTagMap>
            {
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 1
                },
                new PostTagMap
                {
                    PostId = 1,
                    TagId = 3
                },
                new PostTagMap
                {
                    PostId = 2,
                    TagId = 1
                },
                new PostTagMap
                {
                    PostId = 3,
                    TagId = 1
                },
                new PostTagMap
                {
                    PostId = 3,
                    TagId = 2
                },
                new PostTagMap
                {
                    PostId = 3,
                    TagId = 3
                },new PostTagMap
                {
                    PostId = 4,
                    TagId = 3
                },new PostTagMap
                {
                    PostId = 5,
                    TagId = 1
                },new PostTagMap
                {
                    PostId = 5,
                    TagId = 2
                },new PostTagMap
                {
                    PostId = 6,
                    TagId = 2
                },new PostTagMap
                {
                    PostId = 6,
                    TagId = 3
                },new PostTagMap
                {
                    PostId = 7,
                    TagId = 2
                },new PostTagMap
                {
                    PostId = 8,
                    TagId = 1
                }
            });
            modelBuilder.Entity<Comment>().HasData(new List<Comment>
            {
                new Comment
                {
                    CommentHeader = "Nice Post!",
                    CommentText = "Excellent Well",
                    CommentTime = new DateTime(2023,02,04),
                    Email = "toan@gmail.com",
                    Name = "Toan",
                    Id = 1,
                    PostId = 1
                },
                new Comment
                {
                    CommentHeader = "Bad",
                    CommentText = "So Rude",
                    CommentTime = new DateTime(2022,12,03),
                    Email = "josh@gmail.com",
                    Name = "Josh Kas",
                    Id = 2,
                    PostId = 2
                }
            });
        }
    }
}
