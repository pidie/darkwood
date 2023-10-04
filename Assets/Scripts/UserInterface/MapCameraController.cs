using System;
using UnityEngine;

namespace UserInterface
{
    public class MapCameraController : MonoBehaviour
    {
        private Camera _mapCam;
        private int _groundLayer;

        public static Action<bool> OnMapActive;

        private void Awake()
        {
            _mapCam = GetComponent<Camera>();
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void OnEnable() => OnMapActive += ActivateMapCamera;

        private void OnDisable() => OnMapActive -= ActivateMapCamera;

        private void ActivateMapCamera(bool value) => _mapCam.cullingMask = value ? _groundLayer : 0;
    }
}