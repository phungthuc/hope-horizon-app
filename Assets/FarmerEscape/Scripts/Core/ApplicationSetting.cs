using System.Collections;
using UnityEngine;

namespace Game.Core
{
    public class ApplicationSetting : MonoBehaviour
    {
        [SerializeField] private int targetFPS = 60;

        private void Awake()
        {
            Application.targetFrameRate = targetFPS;
        }

        void Start()
        {
            StartCoroutine(StartGame());
        }

        IEnumerator StartGame()
        {
            yield return new WaitForSeconds(0.5f);
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = targetFPS;
        }
    }
}