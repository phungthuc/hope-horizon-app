using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hope_Horizon.Scripts.Components
{
    public class LoadSceneManager : MonoBehaviour
    {
        public void ShowMainScene()
        {
            SceneManager.LoadScene("scene_main");
        }

        public void ShowGameScene()
        {
            SceneManager.LoadScene("scene_game");
        }
    }
}
