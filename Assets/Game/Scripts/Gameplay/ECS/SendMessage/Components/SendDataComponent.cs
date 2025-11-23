using System.Collections.Generic;

namespace Game.Scripts.Gameplay.ECS.SendMessage.Components
{
  public struct SendDataComponent
  {
    public Dictionary<string, object> SendData;
    public float HoldTimer;
  }
}