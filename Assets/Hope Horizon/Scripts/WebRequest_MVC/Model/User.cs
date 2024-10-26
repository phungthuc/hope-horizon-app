using Newtonsoft.Json;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Model
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
