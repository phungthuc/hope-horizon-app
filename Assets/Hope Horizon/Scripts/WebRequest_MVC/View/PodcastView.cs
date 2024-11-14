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
                // item.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = podcast.Title;
                // item.transform.Find("Author").GetComponent<TextMeshProUGUI>().text = podcast.Author.Name;
                // item.transform.Find("Category").GetComponent<TextMeshProUGUI>().text = podcast.Category.Title;

                Button playButton = item.transform.Find("PlayButton").GetComponent<Button>();
                playButton.onClick.AddListener(() => {
                    Debug.Log("Playing podcast: " + podcast.Title);
                    PlayPodcast(podcast.AudioUrl);
                });
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
                    if (clip == null)
                    {
                        feedbackText.text = "Failed to load audio clip.";
                    }
                    else
                    {
                        AudioSource audioSource = GetComponent<AudioSource>();
                        audioSource.clip = clip;
                        audioSource.Play();
                        feedbackText.text = "Playing: " + url;
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
