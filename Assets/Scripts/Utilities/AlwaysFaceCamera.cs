using UnityEngine;

namespace Utilities
{
    public class AlwaysFaceCamera : MonoBehaviour
    {
        private Transform _cam;
 
        private void Awake()
        {
            if (Camera.main != null) _cam = Camera.main.transform;
        }

        private void Update() => transform.LookAt(_cam.position);
    }
}