using Hope_Horizon.Scripts.WebRequest_MVC.Controller;
using TMPro;
using UnityEngine;
using System;

namespace LaserPathPuzzle.Scripts.Components.Screens
{
    public class HomeScreenController: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI welcomeText;

        private void Start()
        {
            SetWelcomeText();
        }

        private void SetWelcomeText()
        {
            // Get current time in Vietnam and set welcome text
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

            int hour = vietnamTime.Hour;

            if (hour >= 5 && hour < 12)
            {
                welcomeText.text = "Good Morning!";
            }
            else if (hour >= 12 && hour < 18)
            {
                welcomeText.text = "Good Afternoon!";
            }
            else if (hour >= 18 && hour < 22)
            {
                welcomeText.text = "Good Evening!";
            }
            else
            {
                welcomeText.text = "Good Night!";
            }
        }
    }
}
