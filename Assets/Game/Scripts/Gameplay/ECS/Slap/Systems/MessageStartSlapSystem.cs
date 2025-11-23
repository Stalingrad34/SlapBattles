using Game.Scripts.Gameplay.ECS.Common;
using Game.Scripts.Gameplay.ECS.Slap.Components;
using Leopotam.Ecs;

namespace Game.Scripts.Gameplay.ECS.Slap.Systems
{
  public class MessageStartSlapSystem : IEcsRunSystem
  {
    private EcsFilter<IdentifierComponent> _filter;
    private EcsFilter<StartSlapMessage> _messages;
    
    public void Run()
    {
      foreach (var i in _messages)
      {
        foreach (var ii in _filter)
        {
          if (_messages.Get1(i).PlayerId != _filter.Get1(ii).Id)
            continue;

          _filter.GetEntity(ii).Get<StartSlapEvent>();
        }
      }
    }
  }
}