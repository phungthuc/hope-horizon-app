using Hope_Horizon.Scripts.WebRequest_MVC.Model;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.WebRequest_MVC.View
{
    public class PostView : MonoBehaviour
    {
        public GameObject postItemPrefab;
        public Transform postListContainer;
        public TextMeshProUGUI feedbackText;
        public PostDetailView postDetailView;
        public UnityEvent onPostsLoaded;

        public void DisplayPosts(List<Post> posts)
        {
            foreach (Post post in posts)
            {
                GameObject item = Instantiate(postItemPrefab, postListContainer);
                item.transform.Find("Author").GetComponent<TextMeshProUGUI>().text = post.Author.Name;
                item.transform.Find("ShortText").GetComponent<TextMeshProUGUI>().text = post.ShortText;

                Button detailsButton = item.transform.Find("DetailsButton").GetComponent<Button>();
                detailsButton.onClick.AddListener(() => {
                    Debug.Log("Displaying post details: " + post.Title);
                    postDetailView.DisplayPostDetails(post);
                    onPostsLoaded.Invoke();
                });

            }
        }

        private void DisplayPostDetails(Post post)
        {
            feedbackText.text = post.LongText;
        }
    }
}
