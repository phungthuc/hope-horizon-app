using Hope_Horizon.Scripts.WebRequest_MVC.Model;
using Hope_Horizon.Scripts.WebRequest_MVC.View;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Controller
{
    public class PostController : MonoBehaviour
    {
        public PostView view;
        private const string baseUrl = Constants.BASE_URL + Constants.POST_URL;

        private void Start()
        {
            StartCoroutine(GetPosts());
        }

        private IEnumerator GetPosts()
        {
            if (PlayerPrefsManager.IsTokenExpired())
            {
                yield return RefreshAccessToken();
            }

            string accessToken = PlayerPrefsManager.GetAccessToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                Debug.LogError("Access token is missing or invalid. Please log in.");
                yield break;
            }

            using (UnityWebRequest request = new UnityWebRequest(baseUrl, "GET"))
            {
                request.SetRequestHeader("Authorization", "Bearer " + accessToken);
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string jsonResponse = request.downloadHandler.text;
                    Debug.Log("API Response: " + jsonResponse);

                    try
                    {
                        var posts = JsonConvert.DeserializeObject<List<Post>>(jsonResponse);
                        view.DisplayPosts(posts);
                    }
                    catch (JsonSerializationException ex)
                    {
                        Debug.LogError("Failed to deserialize JSON: " + ex.Message);
                    }
                }
                else
                {
                    Debug.LogError("Failed to load posts: " + request.error);
                }

            }
        }

        private IEnumerator RefreshAccessToken()
        {
            string refreshToken = PlayerPrefsManager.GetRefreshToken();
            if (string.IsNullOrEmpty(refreshToken))
            {
                Debug.LogError("No refresh token available. Please log in again.");
                yield break;
            }

            var refreshData = new { refresh_token = refreshToken };
            string jsonData = JsonConvert.SerializeObject(refreshData);
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

            using (UnityWebRequest request = new UnityWebRequest(Constants.BASE_URL + "/refresh-token", "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var response = JsonConvert.DeserializeObject<AuthResponse>(request.downloadHandler.text);
                    if (response != null)
                    {
                        PlayerPrefsManager.SetAccessToken(response.Access);
                        float tokenExpiry = Time.time + 3600;
                        PlayerPrefsManager.SetTokenExpiry(tokenExpiry);
                    }
                }
                else
                {
                    Debug.LogError("Failed to refresh access token. Please log in again.");
                }
            }
        }
    }
}
