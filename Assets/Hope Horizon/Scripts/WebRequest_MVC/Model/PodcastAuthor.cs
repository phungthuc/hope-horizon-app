using Newtonsoft.Json;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Model
{
    public class PodcastAuthor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
