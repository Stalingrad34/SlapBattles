using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Spawn.Components
{
  public struct SpawnEnemyEvent
  {
    public string Id;
    public Vector3 Position;
    public Player Player;
  }
}