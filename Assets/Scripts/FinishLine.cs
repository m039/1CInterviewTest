using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FinishLine : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Transform _Visual;

        #endregion

        void Update()
        {
            UpdateVisual();
        }

        void UpdateVisual()
        {
            var scale = _Visual.localScale;
            var viewRect = CameraController.Instance.GetViewRect();

            scale.x = viewRect.width;

            _Visual.localScale = scale;
        }

        public Rect GetRect()
        {
            var p = transform.position;
            var viewRect = CameraController.Instance.GetViewRect();
            var scale = _Visual.localScale;

            return new Rect(viewRect.x, p.y - scale.y / 2f, viewRect.width, scale.y);
        }
    }
}
