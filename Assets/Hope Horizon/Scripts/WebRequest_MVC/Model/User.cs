using Newtonsoft.Json;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Model
{
    public class Profile
    {
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
    }

    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }
}
