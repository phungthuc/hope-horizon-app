using Hope_Horizon.Scripts.WebRequest_MVC.Model;
using Hope_Horizon.Scripts.WebRequest_MVC.View;
using HopeHorizon.Scripts.PlayerPrefs;
using Newtonsoft.Json;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Controller
{
    [System.Serializable]
    public class AuthResponse
    {
        public string Refresh { get; set; }
        public string Access { get; set; }
    }

    public class UserController : MonoBehaviour
    {
        private const string baseUrl = Constants.BASE_URL + Constants.USER_URL;
        public UserView view;

        private void Start()
        {
            if (PlayerPrefsManager.IsUserLoggedIn())
            {
                view.SetFeedback("User is already logged in.");
            }
        }

        public void Register()
        {
            string username = view.GetUsername();
            string password = view.GetPassword();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                view.SetFeedback("Please fill in all fields.");
                return;
            }

            StartCoroutine(RegisterCoroutine(username, password));
        }

        private IEnumerator RegisterCoroutine(string username, string password)
        {
            var userData = new { username, password };
            string jsonData = JsonConvert.SerializeObject(userData);
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

            using (UnityWebRequest request = new UnityWebRequest(baseUrl + "/register/", "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    view.SetFeedback("Registration successful.");
                }
                else
                {
                    view.SetFeedback("Registration failed: " + request.error);
                }
            }
        }

        public void Login()
        {
            PlayerPrefsManager.GetAllKeys();    
            string username = view.GetUsername();
            string password = view.GetPassword();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                view.SetFeedback("Please fill in all fields.");
                return;
            }

            StartCoroutine(LoginCoroutine(username, password));
        }

        private IEnumerator LoginCoroutine(string username, string password)
        {
            var userData = new { username, password };
            string jsonData = JsonConvert.SerializeObject(userData);
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

            using (UnityWebRequest request = new UnityWebRequest(baseUrl + "/login/", "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    OnLoginResponse(request.downloadHandler.text);
                }
                else
                {
                    view.SetFeedback("Login failed: " + request.error);
                }
            }
        }

        private void OnLoginResponse(string jsonResponse)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<AuthResponse>(jsonResponse);

                if (response != null)
                {
                    PlayerPrefsManager.SetAccessToken(response.Access);
                    PlayerPrefsManager.SetRefreshToken(response.Refresh);

                    float tokenExpiry = Time.time + 3600;
                    PlayerPrefsManager.SetTokenExpiry(tokenExpiry);

                    view.SetFeedback("Login successful.");
                }
                else
                {
                    view.SetFeedback("Invalid response from server.");
                }
            }
            catch (System.Exception ex)
            {
                view.SetFeedback("Error processing login response: " + ex.Message);
            }
        }

        public void UpdatePassword()
        {
            string oldPassword = view.GetPassword();
            string newPassword = view.GetNewPassword();

            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                view.SetFeedback("Please fill in all fields.");
                return;
            }

            StartCoroutine(UpdatePasswordCoroutine(oldPassword, newPassword));
        }

        private IEnumerator UpdatePasswordCoroutine(string oldPassword, string newPassword)
        {
            var passwordData = new { old_password = oldPassword, new_password = newPassword };
            string jsonData = JsonConvert.SerializeObject(passwordData);
            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

            using (UnityWebRequest request = new UnityWebRequest(baseUrl + "/update-password/", "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                string accessToken = PlayerPrefsManager.GetAccessToken();
                request.SetRequestHeader("Authorization", "Bearer " + accessToken);

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    view.SetFeedback("Password updated successfully.");
                }
                else
                {
                    view.SetFeedback("Failed to update password: " + request.error);
                }
            }
        }

        public IEnumerator SendProtectedRequest()
        {
            string accessToken = PlayerPrefsManager.GetAccessToken();

            using (UnityWebRequest request = new UnityWebRequest(baseUrl + "/protected-endpoint/", "GET"))
            {
                request.SetRequestHeader("Authorization", "Bearer " + accessToken);
                request.downloadHandler = new DownloadHandlerBuffer();

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    view.SetFeedback("Protected data: " + request.downloadHandler.text);
                }
                else
                {
                    view.SetFeedback("Error: " + request.error);
                }
            }
        }
    }
}
