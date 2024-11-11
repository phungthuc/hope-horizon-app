using UnityEngine;

namespace FarmerEscape.Scripts.Components
{
    public class GameplayResizeer : MonoBehaviour
    {
        [SerializeField] Camera mainCamera;
        private float initialAspect;
        private float initialOrthographicSize;

        void Start()
        {
            initialOrthographicSize = mainCamera.orthographicSize;
            initialAspect = mainCamera.aspect;

            Resize();
        }
    
        void Resize()
        {
            float currentAspect = (float)Screen.width / Screen.height;

            if (Mathf.Abs(currentAspect - 4f / 3f) < 0.05f || Mathf.Abs(currentAspect - 3f / 4f) < 0.05f)
            {
                mainCamera.orthographicSize = initialOrthographicSize * 0.75f;
            }
            else if (Mathf.Abs(currentAspect - 3f / 2f) < 0.05f || Mathf.Abs(currentAspect - 2f / 3f) < 0.05f)
            {
                mainCamera.orthographicSize = initialOrthographicSize * 0.75f;
            }
            else if (Mathf.Abs(currentAspect - 16f / 9f) < 0.05f || Mathf.Abs(currentAspect - 9f / 16f) < 0.05f)
            {
                mainCamera.orthographicSize = initialOrthographicSize * 0.75f;
            }
            else if (Mathf.Abs(currentAspect - 16f / 10f) < 0.05f || Mathf.Abs(currentAspect - 10f / 16f) < 0.05f)
            {
                mainCamera.orthographicSize = initialOrthographicSize * 0.75f;
            }
            else
            {
                if (currentAspect > initialAspect)
                {
                    mainCamera.orthographicSize = initialOrthographicSize * (currentAspect / initialAspect);
                }
                else
                {
                    mainCamera.orthographicSize = initialOrthographicSize / (initialAspect / currentAspect);
                }
            }
        }
    }
}
