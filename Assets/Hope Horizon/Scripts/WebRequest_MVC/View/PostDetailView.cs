using Hope_Horizon.Scripts.WebRequest_MVC.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.WebRequest_MVC.View
{
    public class PostDetailView : MonoBehaviour
    {
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI authorText;
        public TextMeshProUGUI postContentText;
        public Button backButton;

        public void DisplayPostDetails(Post post)
        {
            titleText.text = post.Title;
            authorText.text = post.Author.Name;
            postContentText.text = post.LongText;
        }

        private void ClosePostDetail()
        {
            gameObject.SetActive(false);
        }
    }
}

