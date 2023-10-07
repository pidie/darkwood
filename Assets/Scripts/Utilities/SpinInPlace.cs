using UnityEngine;

namespace Utilities
{
    public class SpinInPlace : MonoBehaviour
    {
        [SerializeField] private Vector3 speed;

        public Vector3 GetSpeed => speed;

        public void SetSpeed(float value) => speed = new Vector3(value, 0f, 0f);
        
        private void FixedUpdate() => transform.Rotate(speed);
    }
}