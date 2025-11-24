using System;

namespace Game.Scripts.Multiplayer
{
  [Serializable]
  public struct SlapPunchInfo
  {
    public string playerId;
    public Vector3Float force;
  }
}