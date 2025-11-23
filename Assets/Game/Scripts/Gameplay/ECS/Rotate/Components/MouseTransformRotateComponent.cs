using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Rotate.Components
{
  public struct MouseTransformRotateComponent
  {
    public Transform Target;
    public float Speed;
    public float MinAngle;
    public float MaxAngle;
    public float CurrentAngle;
  }
}