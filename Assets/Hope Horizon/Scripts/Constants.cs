using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hope_Horizon.Scripts
{
    public static class Constants
    {
        // When deploying to a real server, change this to the server's IP address
        public const string BASE_URL = "http://127.0.0.1:8000/api";
        public const string USER_URL = "/auth";
        public const string POST_URL = "/posts";
        public const string POST_CATE_URL = "/postCates";
    }
}
