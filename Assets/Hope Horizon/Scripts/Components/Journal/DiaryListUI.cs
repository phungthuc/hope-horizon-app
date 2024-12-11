using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hope_Horizon.Scripts.Components.Journal
{
    public class DiaryListUI : MonoBehaviour
    {
        public GameObject diaryItemPrefab;
        public Transform contentPanel;
        public GameObject diaryEntryPanel;
        public Button createDiaryButton;
        public DiaryEntryUI diaryEntryUI;

        private void Start()
        {
            LoadDiaryEntries();
            createDiaryButton.onClick.AddListener(OnCreateNewDiary);
        }

        public void LoadDiaryEntries()
        {
            if (contentPanel == null || diaryItemPrefab == null)
            {
                Debug.LogError("Content Panel hoặc Diary Item Prefab chưa được gán!");
                return;
            }

            foreach (Transform child in contentPanel)
            {
                Destroy(child.gameObject);
            }

            List<DiaryEntry> entries = DiaryManager.GetDiaryEntries();
            if (entries == null || entries.Count == 0)
            {
                Debug.LogWarning("Danh sách nhật ký không có mục nào!");
                return;
            }

            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                GameObject item = Instantiate(diaryItemPrefab, contentPanel);
                // item.GetComponentInChildren<TextMeshProUGUI>().text = $"{entry.DateTime}\n{entry.Content}";
                //limit the number of characters displayed in the diary list
                string contentPreview;
                if (entry.Content.Length > 50)
                {
                    int lastSpaceIndex = entry.Content.LastIndexOf(' ', 50);
                    contentPreview = lastSpaceIndex > 0
                        ? $"{entry.Content.Substring(0, lastSpaceIndex)}..."
                        : $"{entry.Content.Substring(0, 50)}...";
                }
                else
                {
                    contentPreview = entry.Content;
                }

                item.GetComponentInChildren<TextMeshProUGUI>().text = $"{entry.DateTime}\n{contentPreview}";



                Button editButton = item.GetComponentInChildren<Button>();
                if (editButton != null)
                {
                    editButton.onClick.AddListener(() => OnEditDiaryEntry(i, entry));
                }
                else
                {
                    Debug.LogWarning("Nút Edit trong Diary Item Prefab chưa được gán!");
                }
            }
        }

        public void OnEditDiaryEntry(int index, DiaryEntry entry)
        {
            diaryEntryPanel.SetActive(true);
            diaryEntryUI.SetEntry(index, entry);
        }

        public void OnCreateNewDiary()
        {
            diaryEntryPanel.SetActive(true);
            diaryEntryUI.SetNewEntryMode();
        }
    }
}
