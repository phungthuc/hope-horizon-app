using UnityEngine;
using UnityEngine.Events;

namespace HopeHorizon.Scripts.Components
{
    public class DeviceOrientationDetector : MonoBehaviour
    {
        public UnityEvent OrientationChanged;
        private DeviceOrientation _curOrientation;

        private void Update()
        {
            if(Input.deviceOrientation == DeviceOrientation.LandscapeLeft
               || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
                if (_curOrientation != Input.deviceOrientation)
                {
                    _curOrientation = Input.deviceOrientation;
                    OrientationChanged.Invoke();
                }
        }
    }
}
