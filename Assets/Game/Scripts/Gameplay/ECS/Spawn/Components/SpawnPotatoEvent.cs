using UnityEngine;

namespace Game.Scripts.Gameplay.ECS.Spawn.Components
{
  public struct SpawnPotatoEvent
  {
    public string Id;
    public Vector3 Position;
    public Quaternion Rotation;
    public Player Player;
    public string PrefabPath;
    public bool IsPlayer;
  }
}