using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Move.Components
{
  public struct MoveTowardsComponent
  {
    public Vector3 Destination;
    public Vector3 Velocity;
    public float Speed;
  }
}