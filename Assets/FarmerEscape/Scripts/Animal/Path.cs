using System;
using UnityEngine;

namespace FarmerEscape.Scripts.Animal
{
    public class Path : MonoBehaviour
    {
        [SerializeField]
        private Transform[] points;
        
        public Transform[] Points => points;
        
        public Vector3 GetPoint(int index)
        {
            return points[index].position;
        }

        private void OnDrawGizmos()
        {
            if (points == null || points.Length == 0)
            {
                return;
            }
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(points[i].position, 0.1f);
                if (i < points.Length - 1)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(points[i].position, points[i + 1].position);
                }
            }
        }
    }
}
