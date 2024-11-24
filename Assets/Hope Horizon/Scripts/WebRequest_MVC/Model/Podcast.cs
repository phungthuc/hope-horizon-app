using Newtonsoft.Json;
using System;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Model
{
    public class Podcast
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("podcast_cate")]
        public PodcastCategory Category { get; set; }

        [JsonProperty("podcast_author")]
        public PodcastAuthor Author { get; set; }

        [JsonProperty("podcast_cate_id")]
        public int CategoryId { get; set; }

        [JsonProperty("podcast_author_id")]
        public int AuthorId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image_title")]
        public string ImageTitle { get; set; }

        [JsonProperty("content")]
        public string AudioUrl { get; set; }

        public string FullImageUrl => Constants.BASE_URL + ImageTitle;
        public string FullAudioUrl => Constants.BASE_URL + AudioUrl;

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
