using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Hope_Horizon.Scripts.WebRequest_MVC.Model;

namespace Hope_Horizon.Scripts.WebRequest_MVC.Controller
{
    public class PodcastPlayerController : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI podcastTitle;
        public TextMeshProUGUI podcastAuthor;
        public TextMeshProUGUI podcastCategory;
        public Image podcastImage;
        public Slider progressBar;
        public TextMeshProUGUI currentTimeText;
        public TextMeshProUGUI totalTimeText;
        public Button pauseButton;
        public Button resumeButton;

        private AudioSource audioSource;
        private Podcast currentPodcast;
        private bool isPaused;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            pauseButton.onClick.AddListener(PauseAudio);
            resumeButton.onClick.AddListener(ResumeAudio);
            progressBar.onValueChanged.AddListener(SeekAudio);
        }

        public void Initialize(Podcast podcast)
        {
            currentPodcast = podcast;

            // Update UI
            podcastTitle.text = podcast.Title;
            podcastAuthor.text = podcast.Author.Name;
            podcastCategory.text = podcast.Category.Title;
            StartCoroutine(LoadImage(podcast.FullImageUrl));

            StartCoroutine(LoadAndPlayAudio(podcast.FullAudioUrl));
        }

        private IEnumerator LoadImage(string url)
        {
            using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    Texture2D texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(request);
                    podcastImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
            }
        }

        private IEnumerator LoadAndPlayAudio(string url)
        {
            using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    AudioClip clip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(request);
                    audioSource.clip = clip;
                    audioSource.Play();

                    totalTimeText.text = FormatTime(clip.length);
                    StartCoroutine(UpdateProgressBar());
                }
            }
        }

        private IEnumerator UpdateProgressBar()
        {
            while (audioSource.isPlaying || isPaused)
            {
                progressBar.value = audioSource.time / audioSource.clip.length;
                currentTimeText.text = FormatTime(audioSource.time);
                yield return null;
            }
        }

        private void PauseAudio()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                isPaused = true;
            }
        }

        private void ResumeAudio()
        {
            if (isPaused)
            {
                audioSource.Play();
                isPaused = false;
            }
        }

        private void SeekAudio(float value)
        {
            if (audioSource.clip != null)
            {
                audioSource.time = value * audioSource.clip.length;
                currentTimeText.text = FormatTime(audioSource.time);
            }
        }

        private string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
