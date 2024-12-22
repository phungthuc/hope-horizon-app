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
        [SerializeField] private string initialPrompt;

        private List<MessageData> chatHistory = new List<MessageData>();
        private bool isAtBottom = true;

        private void Start()
        {
            if (!string.IsNullOrEmpty(initialPrompt))
            {
                AddInitialPromptToUI();
            }

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

        private void AddInitialPromptToUI()
        {
            apiManager.Send(initialPrompt);
        }

        public void AddMessageToUI(string text, bool isUser)
        {
            GameObject newMessage = Instantiate(messagePrefab, contentParent);

            Transform messageContainer = newMessage.transform.Find("MessageContainer");

            TextMeshProUGUI messageText = messageContainer.Find("Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI timestampText = messageContainer.Find("Timestamp").GetComponent<TextMeshProUGUI>();

            messageText.text = text;
            timestampText.text = DateTime.Now.ToString("HH:mm:ss");

            RectTransform newMessageRect = newMessage.GetComponent<RectTransform>();
            RectTransform messageContainerRect = messageContainer.GetComponent<RectTransform>();
            float textHeight = messageText.preferredHeight;
            newMessageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight + 300);
            messageContainerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight + 300);

            HorizontalLayoutGroup layoutGroup = newMessage.GetComponent<HorizontalLayoutGroup>();

            if (isUser)
            {
                layoutGroup.childAlignment = TextAnchor.MiddleRight;
            }
            else
            {
                layoutGroup.childAlignment = TextAnchor.MiddleLeft;
            }

            Canvas.ForceUpdateCanvases();

            ScrollToTopOfLastMessage();
        }

        public void ScrollToTopOfLastMessage()
        {
            RectTransform lastMessageRect = contentParent.GetChild(contentParent.childCount - 1).GetComponent<RectTransform>();

            ScrollRect scrollRect = contentParent.GetComponentInParent<ScrollRect>();

            if (scrollRect != null && lastMessageRect != null)
            {
                float messagePosY = lastMessageRect.localPosition.y;

                Vector3 newPosition = contentParent.localPosition;
                newPosition.y = -messagePosY;
                contentParent.localPosition = newPosition;
            }
        }
    }
}
