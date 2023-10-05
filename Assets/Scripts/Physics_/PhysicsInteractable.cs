using UnityEngine;

public class PhysicsInteractable : MonoBehaviour, IPhysicsInteractable
{
    [Header("Behaviors")]
    [SerializeField] private UnityEvent standardBehavior;

    // add additional fields for different conditional behaviors here

    private void OnEnable() => PhysicsManager.Instance.OnApplyPhysics += PhysicsBehavior;

    private void OnDisable() => PhysicsManager.Instance.OnApplyPhysics -= PhysicsBehavior;

    private bool PhysicsBehavior()
    {
        // as conditional behaviors are added, check for conditions before invoking methods
        standardBehavior?.Invoke();
    }
}
