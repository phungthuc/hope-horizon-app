using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Hope_Horizon.Scripts.GeminiAI
{
    public class APIManager : MonoBehaviour
    {
        public Action<string> OnResponseReceived;

        private string _prompt;

        // Google Apps Script URL for the GAS API
        private string _gasURL = "https://script.google.com/macros/s/AKfycbyIQlIhTWwuxVe3AFNmmd5QH-iYTeX4ml5M-1C4pyitaeqgZ2AZ2Kz7fSfEJ9e6uJcO/exec";

        public void Send(string prompt)
        {
            _prompt = prompt;
            StartCoroutine(SendDataToGAS());
        }

        private IEnumerator SendDataToGAS()
        {
            WWWForm form = new WWWForm();
            form.AddField("parameter", _prompt);
            UnityWebRequest www = UnityWebRequest.Post(_gasURL, form);

            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                string response = www.downloadHandler.text;
                OnResponseReceived?.Invoke(response);
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }
    }
}
