using Newtonsoft.Json;
using System;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Model
{
    public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("post_cate")]
        public PostCategory Category { get; set; }

        [JsonProperty("post_author")]
        public PostAuthor Author { get; set; }

        [JsonProperty("post_cate_id")]
        public int CategoryId { get; set; }

        [JsonProperty("post_author_id")]
        public int AuthorId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text_short")]
        public string ShortText { get; set; }

        [JsonProperty("text_long")]
        public string LongText { get; set; }

        [JsonProperty("image_title")]
        public string ImageTitle { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class PostCategory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class PostAuthor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
