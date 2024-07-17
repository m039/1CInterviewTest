using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        Camera _camera;

        void Awake()
        {
            Instance = this;

            _camera = GetComponent<Camera>();
        }

        public Rect GetViewRect()
        {
            var height = _camera.orthographicSize * 2;
            var width = height * _camera.aspect;

            return new Rect(-width / 2, -height / 2, width, height);
        }
    }
}
