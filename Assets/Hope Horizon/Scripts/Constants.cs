using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hope_Horizon.Scripts
{
    public static class Constants
    {
        // When deploying to a real server, change this to the server's IP address
        public const string BASE_URL = "http://192.168.1.8:8000/api";
        public const string LOGIN_URL = "/auth/login/";
        public const string REGISTER_URL = "/register/";
        // public const string POST_URL = "/posts";
        // public const string POST_CATE_URL = "/postCates";
    }
}
