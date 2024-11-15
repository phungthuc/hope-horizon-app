using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.Components.Journal
{
    public class DiaryEntryUI : MonoBehaviour
    {
        public TextMeshProUGUI dateText;
        public TMP_InputField contentInputField;
        public Button saveButton;
        private int currentIndex;
        private bool isNewEntry;

        private void Start()
        {
            saveButton.onClick.AddListener(SaveEntry);
        }

        public void SetNewEntryMode()
        {
            isNewEntry = true;
            dateText.text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            contentInputField.text = string.Empty;
        }

        public void SetEntry(int index, DiaryEntry entry)
        {
            isNewEntry = false;
            currentIndex = index - 1;
            dateText.text = entry.DateTime;
            contentInputField.text = entry.Content;
        }

        private void SaveEntry()
        {
            if (isNewEntry)
            {
                DiaryManager.AddDiaryEntry(contentInputField.text);
            }
            else
            {
                DiaryManager.UpdateDiaryEntry(currentIndex, contentInputField.text);
            }

            FindObjectOfType<DiaryListUI>().LoadDiaryEntries();

            gameObject.SetActive(false);
        }
    }
}
