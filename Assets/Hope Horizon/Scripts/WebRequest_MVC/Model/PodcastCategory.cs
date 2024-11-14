using Newtonsoft.Json;
using System;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Model
{
    public class PodcastCategory
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
}
