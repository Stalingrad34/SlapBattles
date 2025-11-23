using System.Collections.Generic;
using Colyseus.Schema;

namespace Game.Scripts.Gameplay.ECS.Common
{
  public struct PlayerChangeEvent
  {
    public string Id;
    public List<DataChange> Changes;
  }
}