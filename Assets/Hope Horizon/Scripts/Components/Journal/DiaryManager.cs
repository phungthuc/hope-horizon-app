using System.Collections.Generic;
using UnityEngine;

namespace Hope_Horizon.Scripts.Components.Journal
{
    public static class DiaryManager
    {
        private const string DiaryEntriesKey = "DiaryEntries";

        public static void SaveDiaryEntries(List<DiaryEntry> entries)
        {
            if (entries == null || entries.Count == 0)
            {
                Debug.LogError("Không có nhật ký để lưu.");
                return;
            }

            string json = JsonUtility.ToJson(new DiaryEntryListWrapper(entries));
            PlayerPrefs.SetString(DiaryEntriesKey, json);
            PlayerPrefs.Save();
            Debug.Log("Đã lưu nhật ký.");
        }

        public static List<DiaryEntry> GetDiaryEntries()
        {
            string json = PlayerPrefs.GetString(DiaryEntriesKey, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("Không tìm thấy dữ liệu nhật ký trong PlayerPrefs.");
                return new List<DiaryEntry>();
            }

            DiaryEntryListWrapper wrapper = JsonUtility.FromJson<DiaryEntryListWrapper>(json);
            return wrapper.Entries ?? new List<DiaryEntry>();
        }

        public static void AddDiaryEntry(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                Debug.LogError("Nội dung nhật ký không hợp lệ.");
                return;
            }

            var entries = GetDiaryEntries();
            entries.Add(new DiaryEntry(content));
            SaveDiaryEntries(entries);
            Debug.Log("Đã thêm nhật ký mới.");
        }

        public static void UpdateDiaryEntry(int index, string newContent)
        {
            Debug.Log("Cập nhật nhật ký bắt đầu.");
            Debug.Log("Index: " + index);
            Debug.Log("Nội dung mới: " + newContent);

            if (index < 0 || string.IsNullOrEmpty(newContent))
            {
                Debug.LogError("Index không hợp lệ hoặc nội dung mới không hợp lệ.");
                return;
            }

            var entries = GetDiaryEntries();

            if (index >= 0 && index < entries.Count)
            {
                DiaryEntry entryToUpdate = entries[index];
                Debug.Log("Nội dung cũ: " + entryToUpdate.Content);

                entryToUpdate.Content = newContent;
                entryToUpdate.DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                SaveDiaryEntries(entries);
                Debug.Log($"Cập nhật nhật ký tại index {index}: {entryToUpdate.Content}");
            }
            else
            {
                Debug.LogError($"Không tìm thấy nhật ký với index {index}. Số lượng nhật ký hiện tại: {entries.Count}");
            }
        }

        [System.Serializable]
        private class DiaryEntryListWrapper
        {
            public List<DiaryEntry> Entries;

            public DiaryEntryListWrapper(List<DiaryEntry> entries)
            {
                Entries = entries;
            }
        }
    }
}
