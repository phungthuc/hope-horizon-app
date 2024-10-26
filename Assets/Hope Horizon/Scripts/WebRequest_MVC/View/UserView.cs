using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.WebRequest_MVC.View
{
    public class UserView : MonoBehaviour
    {

        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public TMP_InputField newPasswordInput;
        public TextMeshProUGUI feedbackText;

        public string GetUsername() => usernameInput.text;
        public string GetPassword() => passwordInput.text;
        public string GetNewPassword() => newPasswordInput.text;

        public void SetFeedback(string message) => feedbackText.text = message;
    }
}
