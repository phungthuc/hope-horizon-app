using Hope_Horizon.Scripts.WebRequest_MVC.Model;
using Hope_Horizon.Scripts.WebRequest_MVC.View;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Controller
{
    public class PodcastController : MonoBehaviour
    {
        public PodcastView view;
        private const string baseUrl = Constants.BASE_URL + Constants.PODCAST_URL;

        private void Start()
        {
            StartCoroutine(GetPodcasts());
        }

        private IEnumerator GetPodcasts()
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

            using (UnityWebRequest request = new UnityWebRequest(Constants.BASE_URL + Constants.PODCAST_URL, "GET"))
            {
                request.SetRequestHeader("Authorization", "Bearer " + accessToken);
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var podcasts = JsonConvert.DeserializeObject<List<Podcast>>(request.downloadHandler.text);
                    view.DisplayPodcasts(podcasts);
                }
                else
                {
                    Debug.LogError("Failed to load podcasts: " + request.error);
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
                        float tokenExpiry = Time.time + 3600; // Ví dụ, 3600 giây = 1 giờ
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
