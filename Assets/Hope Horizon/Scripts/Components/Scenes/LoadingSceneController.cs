using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Hope_Horizon.Scripts.Components
{
    public class LoadingSceneController : MonoBehaviour
    {
        public UnityEvent Loaded;
        void Start()
        {
            StartCoroutine(LoadEssentialData());
        }

        private IEnumerator LoadEssentialData()
        {
            bool isLoggedIn = CheckLoginStatus();

            yield return StartCoroutine(LoadAIConfiguration());

            // yield return StartCoroutine(LoadMiniGamesAssets());

            // yield return StartCoroutine(LoadPodcastAndNewsData());

            // yield return StartCoroutine(LoadUIAssets());

            Loaded?.Invoke();
            SceneManager.LoadScene(isLoggedIn ? "scene_main" : "scene_login");
        }

        private bool CheckLoginStatus()
        {
            return PlayerPrefs.HasKey("refreshToken");
        }

        private IEnumerator LoadAIConfiguration()
        {
            Debug.Log("Loading AI configuration...");
            yield return new WaitForSeconds(1);

            bool aiConfigLoaded = LoadAIModel();
            if (aiConfigLoaded)
            {
                Debug.Log("AI configuration loaded successfully");
            }
        }

        private bool LoadAIModel()
        {
            return true;
        }

        private IEnumerator LoadMiniGamesAssets()
        {
            Debug.Log("Loading mini-games assets...");

            string bundlePath = "path/to/mini_games.bundle";
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
            if (bundle != null)
            {
                Debug.Log("Mini-games assets loaded successfully");
            }
            else
            {
                Debug.LogError("Failed to load mini-games assets");
            }

            yield return null;
        }

        private IEnumerator LoadPodcastAndNewsData()
        {
            Debug.Log("Loading podcast and news data...");

            yield return new WaitForSeconds(1);

            string[] podcasts = { "Podcast 1", "Podcast 2", "Podcast 3" };
            string[] news = { "Positive News 1", "Positive News 2", "Positive News 3" };

            Debug.Log("Podcast and news data loaded successfully");
        }

        private IEnumerator LoadUIAssets()
        {
            Debug.Log("Loading UI assets...");

            Sprite loadingIcon = Resources.Load<Sprite>("Icons/loading_icon");
            Texture2D backgroundTexture = Resources.Load<Texture2D>("Textures/background");

            if (loadingIcon != null && backgroundTexture != null)
            {
                Debug.Log("UI assets loaded successfully");
            }
            else
            {
                Debug.LogError("Failed to load some UI assets");
            }

            yield return null;
        }
    }
}
