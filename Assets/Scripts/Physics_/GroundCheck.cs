using UnityEngine;

namespace Physics_
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private float radius = 0.4f;
        [SerializeField] private Vector3 box;
        [SerializeField] private bool usePoint = true;
        
        private Transform checker;
        private LayerMask layerMask;

        private void Awake()
        {
            if (checker == null) checker = transform;

            if (layerMask == default)
                layerMask = 1 << LayerMask.NameToLayer("Ground");
        }

        public bool CheckIsGrounded() => usePoint 
            ? Physics.CheckSphere(checker.position, radius, layerMask) 
            : Physics.CheckBox(checker.position, box, transform.rotation, layerMask);
    }
}