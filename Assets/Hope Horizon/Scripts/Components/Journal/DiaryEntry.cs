namespace Hope_Horizon.Scripts.Components.Journal
{
    [System.Serializable]
    public class DiaryEntry
    {
        public string DateTime;
        public string Content;

        public DiaryEntry(string content)
        {
            DateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Content = content;
        }
    }
}
