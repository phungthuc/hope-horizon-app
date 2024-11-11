using UnityEngine;
using UnityEngine.Events;

namespace FarmerEscape.Scripts.Inputs
{
    public class Pickable : MonoBehaviour
    {
        public UnityEvent OnPointerDown;
        public UnityEvent<Vector2> OnPointerMove;
        public UnityEvent OnPointerUp;
        public UnityEvent OnPointerExit;
        public void PointerDown()
        {
            OnPointerDown.Invoke();
        }

        public void PointerMove(Vector2 position)
        {
            OnPointerMove.Invoke(position);
        }

        public void PointerUp()
        {
            OnPointerUp.Invoke();
        }

        public void PointerExit()
        {
            OnPointerExit.Invoke();
        }
    }
}
