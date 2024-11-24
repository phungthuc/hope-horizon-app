using Hope_Horizon.Scripts.WebRequest_MVC.Controller;
using Hope_Horizon.Scripts.WebRequest_MVC.Model;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Hope_Horizon.Scripts.WebRequest_MVC.View
{
    public class PodcastView : MonoBehaviour
    {
        [SerializeField]
        private GameObject podcastPlayerPrefab;

        public GameObject podcastItemPrefab;
        public Transform podcastListContainer;
        public TextMeshProUGUI feedbackText;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.volume = 1.0f;
        }

        public void DisplayPodcasts(List<Podcast> podcasts)
        {
            foreach (Podcast podcast in podcasts)
            {
                GameObject item = Instantiate(podcastItemPrefab, podcastListContainer);
                item.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = podcast.Title;
                item.transform.Find("Author").GetComponent<TextMeshProUGUI>().text = podcast.Author.Name;
                item.transform.Find("Category").GetComponent<TextMeshProUGUI>().text = podcast.Category.Title;

                // Image image = item.transform.Find("Thumbnail").GetComponent<Image>();
                // StartCoroutine(LoadImage(podcast.FullImageUrl, image));

                Button playButton = item.transform.Find("PlayButton").GetComponent<Button>();
                playButton.onClick.AddListener(() => {
                    Debug.Log("Opening podcast player: " + podcast.Title);
                    OpenPodcastPlayer(podcast);
                });
            }
        }

        public void OpenPodcastPlayer(Podcast podcast)
        {
            podcastPlayerPrefab.SetActive(true);

            PodcastPlayerController playerController = podcastPlayerPrefab.GetComponent<PodcastPlayerController>();
            playerController.Initialize(podcast);
        }


        private IEnumerator LoadImage(string url, Image image)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
                else
                {
                    Debug.LogError("Failed to load image: " + www.error);
                }
            }
        }


        private void PlayPodcast(string audioUrl)
        {
            StartCoroutine(StreamAudio(audioUrl));
        }

        private IEnumerator StreamAudio(string url)
        {
            Debug.Log("Attempting to load audio from: " + url);

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                    if (clip != null)
                    {
                        audioSource.Stop();
                        audioSource.clip = clip;
                        audioSource.Play();
                        feedbackText.text = "Playing: " + url;
                    }
                    else
                    {
                        feedbackText.text = "Failed to load audio clip.";
                    }
                }
                else
                {
                    feedbackText.text = "Error loading audio: " + www.error;
                    Debug.LogError("Error loading audio: " + www.error);
                }
            }
        }
    }
}
