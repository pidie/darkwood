using UnityEngine;

public class PhysicsInteractable : MonoBehaviour, IPhysicsInteractable
{
    [Header("Behaviors")]
    [SerializeField] private UnityEvent standardBehavior;

    private bool PhysicsBehavior()
    {
        standardBehavior?.Invoke();
    }
}
