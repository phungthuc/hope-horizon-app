using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.GeminiAI
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private GameObject messagePrefab;
        [SerializeField] private Transform contentParent;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button sendButton;
        [SerializeField] private APIManager apiManager;

        private List<MessageData> chatHistory = new List<MessageData>();

        private void Start()
        {
            sendButton.onClick.AddListener(SendMessage);
            apiManager.OnResponseReceived += OnDoctorResponseReceived;
        }

        private void OnDoctorResponseReceived(string response)
        {
            AddMessageToUI(response, false);
        }

        private void SendMessage()
        {
            string messageText = inputField.text.Trim();
            if (string.IsNullOrEmpty(messageText)) return;

            AddMessageToUI(messageText, true);
            inputField.text = "";

            apiManager.Send(messageText);
        }

        public void AddMessageToUI(string text, bool isUser)
        {
            GameObject newMessage = Instantiate(messagePrefab, contentParent);

            Transform messageContainer = newMessage.transform.Find("MessageContainer");

            TextMeshProUGUI messageText = messageContainer.Find("Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timestampText = messageContainer.Find("Timestamp").GetComponent<TextMeshProUGUI>();

            messageText.text = text;
            timestampText.text = DateTime.Now.ToString("HH:mm:ss");

            HorizontalLayoutGroup layoutGroup = newMessage.GetComponent<HorizontalLayoutGroup>();

            if (isUser)
            {
                layoutGroup.childAlignment = TextAnchor.MiddleRight;
            }
            else
            {
                layoutGroup.childAlignment = TextAnchor.MiddleLeft;

                // Scroll to bottom when a new message is added
                this.ScrollToBottom();
            }

        }

        public void ScrollToBottom()
        {
            ScrollRect scrollRect = contentParent.GetComponentInParent<ScrollRect>();
            if (scrollRect != null)
            {
                scrollRect.verticalNormalizedPosition = 0f;
            }
        }


    }

}
