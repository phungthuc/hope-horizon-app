using TMPro;
using UnityEngine;

namespace Hope_Horizon.Scripts.WebRequest_MVC.View
{
    public class UserView : MonoBehaviour
    {
        // Sign up screen
        public TMP_InputField lastNameInput;
        public TMP_InputField firstNameInput;
        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public TMP_InputField emailInput;
        public TMP_InputField dateOfBirthInput;

        // Login screen
        public TMP_InputField loginUsernameInput;
        public TMP_InputField loginPasswordInput;

        // Update password screen
        public TMP_InputField oldPasswordInput;
        public TMP_InputField newPasswordInput;


        // Feedback text
        public TextMeshProUGUI feedbackText;

        // Getters for input fields on the sign up screen
        public string GetLastName() => lastNameInput.text;
        public string GetFirstName() => firstNameInput.text;
        public string GetUsername() => usernameInput.text;
        public string GetPassword() => passwordInput.text;
        public string GetEmail() => emailInput.text;
        public string GetDateOfBirth() => dateOfBirthInput.text;

        // Getters for input fields on the login screen
        public string GetLoginUsername() => loginUsernameInput.text;
        public string GetLoginPassword() => loginPasswordInput.text;

        // Getters for input fields on the update password screen
        public string GetOldPassword() => oldPasswordInput.text;
        public string GetNewPassword() => newPasswordInput.text;

        // Set feedback text
        public void SetFeedback(string message) => feedbackText.text = message;
    }
}
