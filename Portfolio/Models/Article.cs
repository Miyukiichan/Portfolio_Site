using System.Text.Json;

namespace Portfolio.Models
{
    public class Article
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string ArticleFile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Article>(this);
    }
}
