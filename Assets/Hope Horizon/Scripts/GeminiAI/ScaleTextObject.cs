using TMPro;
using UnityEngine;

namespace Hope_Horizon.Scripts.GeminiAI
{
    public class ScaleTextObject: MonoBehaviour
    {
        public TextMeshProUGUI textMeshProUGUI;

        void Start()
        {
            float textHeight = textMeshProUGUI.preferredHeight;

            RectTransform rectTransform = textMeshProUGUI.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight);
        }
    }
}
