using UnityEngine;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.Components.SOS
{
    public class SOSButton : MonoBehaviour
    {
        public Button sosButton;

        private void Start()
        {
            if (sosButton != null)
            {
                sosButton.onClick.AddListener(OnSOSButtonClicked);
            }
        }

        private void OnSOSButtonClicked()
        {
            string phoneNumber = "tel:911";

            Application.OpenURL(phoneNumber);
        }
    }
}
