using System;
using UnityEngine;

namespace FarmerEscape.Scripts.Animal
{
    public enum EndAction
    {
        Stop,
        Loop,
        Reverse
    }
    public class MoveWithPath : MonoBehaviour
    {
        public Path pathToFollow;
        public float speed = 5.0f;
        public EndAction endAction;
        
        private int _currentPoint;
        private Vector3 _targetPoint;

        private void Update()
        {
            if (pathToFollow == null)
            {
                return;
            }
            if (_currentPoint >= pathToFollow.Points.Length)
            {
                switch (endAction)
                {
                    case EndAction.Stop:
                        enabled = false;
                        break;
                    case EndAction.Loop:
                        _currentPoint = 0;
                        break;
                    case EndAction.Reverse:
                        Array.Reverse(pathToFollow.Points);
                        _currentPoint = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            _targetPoint = pathToFollow.GetPoint(_currentPoint);
            float distance = Vector3.Distance(transform.position, _targetPoint);
            if (distance < 0.1f)
            {
                _currentPoint++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPoint, speed * Time.deltaTime);
                transform.up = _targetPoint - transform.position;
            }
        }
    }
}
