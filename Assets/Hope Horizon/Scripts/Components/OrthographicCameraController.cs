using UnityEngine;
using UnityEngine.Events;

namespace HopeHorizon.Scripts.Components
{
    [RequireComponent(typeof(Camera))]
    public class OrthographicCameraController : MonoBehaviour
    {
        public float orthoWidth = 11.25f;

        private float _originalOrthoSize;
        private Camera _camera;

        public UnityEvent OnCameraOrthoSizeChanged;
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _originalOrthoSize = _camera.orthographicSize;
        }

        public void RevertToOriginalSize()
        {
            if (!_camera)
            {
                _camera = GetComponent<Camera>();
            }
            _camera.orthographicSize = _originalOrthoSize;
            OnCameraOrthoSizeChanged?.Invoke();
        }

        public void FitToScreen()
        {
            if (!_camera)
            {
                _camera = GetComponent<Camera>();
            }
            _camera.orthographicSize = orthoWidth / Screen.width * Screen.height;
            OnCameraOrthoSizeChanged?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            var height = 30f;
            var width = 22.5f;
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
        }
    }
}
