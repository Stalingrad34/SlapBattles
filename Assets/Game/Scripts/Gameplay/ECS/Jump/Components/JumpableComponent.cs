using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Jump.Components
{
  public struct JumpableComponent
  {
    public float JumpForce;
    public float CheckSphereRadius;
    public LayerMask CheckSphereMask;
    public bool IsFly;
    public float LastChanceJumpTime;
    public float LastChanceJumpTimer;
    public float IntervalJumpTime;
    public float IntervalJumpTimer;
  }
}