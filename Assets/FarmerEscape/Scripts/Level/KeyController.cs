using System;
using DG.Tweening;
using UnityEngine;

namespace FarmerEscape.Scripts.Level
{
    public class KeyController : MonoBehaviour
    {
        [SerializeField] LayerMask animalLayer;
        [SerializeField] DOTweenAnimation doTweenAnimation;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & animalLayer.value) != 0)
            {
                doTweenAnimation.DOPlay();
            }
        }
    }
}
