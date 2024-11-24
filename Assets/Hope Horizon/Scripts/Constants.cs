using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hope_Horizon.Scripts
{
    public static class Constants
    {
        // When deploying to a real server, change this to the server's IP address
        public const string BASE_URL = "http://192.168.1.9:8000";
        public const string LOGIN_URL = "/api/auth/login/";
        public const string REGISTER_URL = "/api/register/";
        public const string PODCAST_URL = "/api/podcast/podcast_index_get_all_api/";
        public const string POST_URL = "/api/post/post_index_get_all_api/";
    }
}
