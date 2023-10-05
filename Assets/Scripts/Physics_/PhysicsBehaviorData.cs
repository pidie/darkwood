using UnityEngine;

public class PhysicsBehaviorData : ScriptableObject
{
    public float gravityMultiplier = 1f;
    [Tooltip("Measured in units per second squared")]
    public float terminalVelocity;
    // add a variable to control sway - use DOTween to control sway as it falls (i.e. - a feather or a leaf falling)
}
