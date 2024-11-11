using UnityEngine;

namespace FarmerEscape.Scripts.Inputs
{
    public class TouchPickingController : MonoBehaviour
    {
        public LayerMask targetMask;

        [SerializeField]
        private Camera mainCamera;

        public Pickable CurrentPicked { get; private set; }
        public void OnPointerDown(Touch touch)
        {
            Component hitCollider = GetClosestColliderAtScreenPoint(touch.position);
            if (hitCollider != null)
            {
                var pickable = hitCollider.GetComponent<Pickable>();
                pickable.PointerDown();
                CurrentPicked = pickable;
            }
        }

        public void OnPointerMove(Touch touch)
        {
            Component hitCollider = GetClosestColliderAtScreenPoint(touch.position);
            if (hitCollider != null)
            {
                var pickable = hitCollider.GetComponent<Pickable>();
                pickable.PointerMove(touch.position);
                if (CurrentPicked && CurrentPicked != pickable)
                {
                    CurrentPicked.PointerExit();
                }
                CurrentPicked = pickable;
            }
            else
            {
                if (CurrentPicked)
                {
                    CurrentPicked.PointerExit();
                    CurrentPicked = null;
                }
            }
        }

        public void OnPointerUp(Touch touch)
        {
            Component hitCollider = GetClosestColliderAtScreenPoint(touch.position);
            if (hitCollider != null)
            {
                var pickable = hitCollider.GetComponent<Pickable>();
                pickable.PointerUp();
                if (CurrentPicked && CurrentPicked != pickable)
                {
                    CurrentPicked.PointerExit();
                }
                CurrentPicked = null;
            }
            else
            {
                if (CurrentPicked)
                {
                    CurrentPicked.PointerExit();
                    CurrentPicked = null;
                }
            }
        }

        private Component GetClosestColliderAtScreenPoint(Vector3 screenPoint)
        {
            Component hitCollider = null;
            float hitDistance = float.MaxValue;
            Ray camRay = mainCamera.ScreenPointToRay(screenPoint);
            RaycastHit hitInfo;
            if (Physics.Raycast(camRay, out hitInfo, Mathf.Infinity, targetMask) == true)
            {
                hitDistance = hitInfo.distance;
                hitCollider = hitInfo.collider;
            }

            RaycastHit2D hitInfo2D = Physics2D.Raycast(camRay.origin, camRay.direction, Mathf.Infinity, targetMask);
            if (hitInfo2D == true)
            {
                if (hitInfo2D.distance < hitDistance)
                {
                    hitCollider = hitInfo2D.collider;
                }
            }

            return (hitCollider);
        }
    }
}
