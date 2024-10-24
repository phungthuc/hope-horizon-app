using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HopeHorizon.Scripts.Components
{
    public class BackgroundScaler : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Camera mainCamera;

        private void Start()
        {
            UpdateScale();
        }

        private void UpdateScale()
        {
            var botLeftScreen = new Vector3(0, 0, mainCamera.nearClipPlane);
            var topRightScreen = new Vector3(Screen.width, Screen.height,mainCamera.nearClipPlane);

            var botLeftWorld = mainCamera.ScreenToWorldPoint(botLeftScreen);
            var topRightWorld = mainCamera.ScreenToWorldPoint(topRightScreen);

            var width = topRightWorld.x - botLeftWorld.x;
            var height = topRightWorld.y - botLeftWorld.y;

            var scaleByWidth = (width / spriteRenderer.bounds.size.x) * transform.localScale.x;
            var scaleByHeight = (height / spriteRenderer.bounds.size.y) * transform.localScale.y;
            var scale = Mathf.Max(scaleByWidth, scaleByHeight);
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}


