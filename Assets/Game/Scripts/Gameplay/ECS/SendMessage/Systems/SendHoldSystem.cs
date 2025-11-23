using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.SendMessage.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.SendMessage.Systems
{
  public class SendHoldSystem : IEcsRunSystem
  {
    private EcsFilter<SendDataComponent> _filter;
    private EcsFilter<RestartEvent> _eventFilter;
    
    public void Run()
    {
      foreach (var i in _eventFilter)
      {
        foreach (var ii in _filter)
        {
          _filter.Get1(ii).HoldTimer = 1;
        }
      }
    }
  }
}